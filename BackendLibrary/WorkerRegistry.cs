using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net.Quic;

namespace BackendLibrary
{
    public class WorkerRegistry 
    {
        Dictionary<int, IWorker> registry;
        private DateTime? lastBackupTime;

        public WorkerRegistry()
        {
            registry = new Dictionary<int, IWorker>();

            //Läser in senaste backup-tid från fil
            if (File.Exists("BackupTime.txt"))
            {
                string savedTime = File.ReadAllText("BackupTime.txt");
                if (DateTime.TryParse(savedTime, out DateTime parsed))
                    lastBackupTime = parsed;
            }
        }

        public List<IWorker> GetAllWorkers()
        {
            // Returnera alla värden (IWorker objekt) från din dictionary 'registry' som en lista.
            return registry.Values.ToList();
        }

        public bool AddWorker(IWorker worker)
        {

            // 1. Kontrollera om ID redan finns
            if (registry.ContainsKey(worker.GetId()))
            {
                return false; // ID finns redan
            }

            // 2. Lägg till arbetaren
            registry.Add(worker.GetId(), worker);
            return true; // Lyckades lägga till
        }
        public bool AddWorker(int id, IWorker worker)
        {
            return registry.TryAdd(id, worker);
        }
        public bool RemoveWorker(int id)
        {
            return registry.Remove(id);
        }
        public bool UpdateWorkerName(int id, string newName)
        {
            // Försök hämta den gamla arbetaren på ett säkert sätt
            if (registry.TryGetValue(id, out IWorker oldWorker))
            {
                // Se till att det är en Ant-instans (om du bara har Ant-objekt i registret)
                if (oldWorker is Ant old)
                {
                    // Skapa en helt ny Ant-instans med det uppdaterade namnet
                    var updated = new Ant(
                       id: old.GetId(),
                       name: newName, // Uppdaterat Namn
                       workType: old.GetWorkType(),
                       shiftType: old.GetShiftType(),
                       workShoes: old.GetWorkShoes(),
                       startDate: old.GetStartDate()
                   );
                    registry[id] = updated; // Ersätt den gamla instansen i Dictionary
                    return true; // Uppdatering lyckades
                }
            }
            return false; // Arbetare hittades inte eller är inte av typen Ant
        }

        // Uppdaterar Skift
        public bool UpdateWorkerShift(int id, ShiftType newShift)
        {
            if (registry.TryGetValue(id, out IWorker oldWorker))
            {
                if (oldWorker is Ant old)
                {
                    var updated = new Ant(
                       id: old.GetId(),
                       name: old.GetName(),
                       workType: old.GetWorkType(),
                       shiftType: newShift, // Uppdaterat Skift
                       workShoes: old.GetWorkShoes(),
                       startDate: old.GetStartDate()
                   );
                    registry[id] = updated;
                    return true;
                }
            }
            return false;
        }

        // Uppdaterar Jobbtyp
        public bool UpdateWorkerType(int id, WorkType newType)
        {
            if (registry.TryGetValue(id, out IWorker oldWorker))
            {
                if (oldWorker is Ant old)
                {
                    var updated = new Ant(
                       id: old.GetId(),
                       name: old.GetName(),
                       workType: newType, // Uppdaterad Jobbtyp
                       shiftType: old.GetShiftType(),
                       workShoes: old.GetWorkShoes(),
                       startDate: old.GetStartDate()
                   );
                    registry[id] = updated;
                    return true;
                }
            }
            return false;
        }

        // Uppdaterar Skyddsskor (Denna anropar SetWorkShoes i Ant.cs om du vill behålla den logiken)
        // Jag ändrar denna till att matcha ditt mönster för konsekvensens skull:
        public bool UpdateWorkerShoes(int id, bool hasShoes)
        {
            if (registry.TryGetValue(id, out IWorker oldWorker))
            {
                if (oldWorker is Ant old)
                {
                    var updated = new Ant(
                       id: old.GetId(),
                       name: old.GetName(),
                       workType: old.GetWorkType(),
                       shiftType: old.GetShiftType(),
                       workShoes: hasShoes, // Uppdaterade Skyddsskor
                       startDate: old.GetStartDate()
                   );
                    registry[id] = updated;
                    return true;
                }
            }
            return false;
        }
        public List<IWorker> SearchWorker(
            string? name = null,
            WorkType? workType = null,
            ShiftType? shiftType = null,
            bool? workShoes = null,
            DateTime? startDate = null,
            TimeSortOptions option = TimeSortOptions.Specified
            )
        {
            var query = registry.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Value.GetName().Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            if(workType != null)
            {
                query = query.Where(p => p.Value.GetWorkType() == workType);
            }
            if (shiftType != null)
            {
                query = query.Where(p => p.Value.GetShiftType() == shiftType);
            }
            if (workShoes != null)
            {
                query = query.Where(p => p.Value.GetWorkShoes() == workShoes);
            }
            if(startDate != null && option == TimeSortOptions.Specified)
            {
                query = query.Where(p => p.Value.GetStartDate().Date == startDate.Value.Date);
            }
            if (startDate != null && option == TimeSortOptions.Before)
            {
                query = query.Where(p => p.Value.GetStartDate().Date < startDate.Value.Date);
            }
            if (startDate != null && option == TimeSortOptions.After)
            {
                query = query.Where(p => p.Value.GetStartDate().Date > startDate.Value.Date);
            }

            List<IWorker> workers = query.Select(p => p.Value).ToList();
            return workers;
        }


        public IWorker SearchWorker(int id)
        {
            if (registry.TryGetValue(id, out IWorker worker))
            {
                return worker;
            }
            return null; // Returnerar null om arbetaren inte hittades
        }
        public void CreateBackup()
        { 
            var lines = new List<string>();
            foreach (var item in registry)
            {
                lines.Add($"{item.Value.GetId()};{item.Value.GetName()};{item.Value.GetWorkType()};{item.Value.GetShiftType()};{item.Value.GetWorkShoes()};{item.Value.GetStartDate()}");
            }
            File.WriteAllLines("Backup.csv", lines);

            //Uppdatera tid
            lastBackupTime = DateTime.Now;
            File.WriteAllText("BackupTime.txt", lastBackupTime.ToString());

            Console.WriteLine($"Backup skapad {lastBackupTime}");
        }
        public void LoadBackup()
        {
            if (File.Exists("Backup.csv"))
            {
                registry = new Dictionary<int, IWorker>();
                string[] lines = File.ReadAllLines("Backup.csv");

                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    AddWorker(int.Parse(parts[0]), new Ant(int.Parse(parts[0]), parts[1], (WorkType)Enum.Parse(typeof(WorkType), parts[2]), (ShiftType)Enum.Parse(typeof(ShiftType), parts[3]), bool.Parse(parts[4]), DateTime.Parse(parts[5]))); //fixa så att det inte bara är myror som läggs till
                }
            }
        }



        /* TEST CODE */
        public void TestPrinter()
        {
            foreach (var item in registry)
            {
                Console.WriteLine(item.Value.ToString());
            }
        }
    }
}
