using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net.Quic;

// Lägg till factory pattern för att kunna skapa nya arbetartyper utan att röra koden i registret. Kolla upp. 
// Ev strategy patttern fö att kunna spara i andra backup format. exempelvis JSON. 
// Ev Singleton Pattern för att kunna göra enbart EN redistry instans. 

// Kolla Solid också, behöver kanske göra om registry klassen. 

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
        public bool AddWorker(int id, IWorker worker)
        {
            return registry.TryAdd(id, worker);
        }
        public bool RemoveWorker(int id)
        {
            return registry.Remove(id);
        }       
        public void UpdateWorkerName(int id, string newName)
        {
            var old = (Ant)registry[id];
            var updated = new Ant(
                id: old.GetId(),
                name: newName,
                workType: old.GetWorkType(),
                shiftType: old.GetShiftType(),
                workShoes: old.GetWorkShoes(),
                startDate: old.GetStartDate()
            );
            registry[id] = updated;
        }
        public void UpdateWorkerShift(int id, ShiftType shift)
        {
            var old = (Ant)registry[id];
            var updated = new Ant(
                id: old.GetId(),
                name: old.GetName(),
                workType: old.GetWorkType(),
                shiftType: shift,
                workShoes: old.GetWorkShoes(),
                startDate: old.GetStartDate()
            );
            registry[id] = updated;
        }
        public void UpdateWorkerShoes(int id, bool hasShoes)
        {
            var old = (Ant)registry[id];
            var updated = new Ant(
                id: old.GetId(),
                name: old.GetName(),
                workType: old.GetWorkType(),
                shiftType: old.GetShiftType(),
                workShoes: hasShoes,
                startDate: old.GetStartDate()
            );
            registry[id] = updated;
        }
        public void UpdateWorkerType(int id, WorkType work)
        {
            var old = (Ant)registry[id];
            var updated = new Ant(
                id: old.GetId(),
                name: old.GetName(),
                workType: work,
                shiftType: old.GetShiftType(),
                workShoes: old.GetWorkShoes(),
                startDate: old.GetStartDate()
            );
            registry[id] = updated;
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
        public bool  SearchWorker(int id, out IWorker worker)
        {
            return registry.TryGetValue(id, out worker);
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
