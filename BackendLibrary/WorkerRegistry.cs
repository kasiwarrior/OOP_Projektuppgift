using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BackendLibrary
{
    public class WorkerRegistry : TimeManagement
    {
        Dictionary<int, IWorker> registry;



        public bool AddWorker(int id, IWorker worker)
        {
            registry.Add(id, worker);
            return true; // Add error detection
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



        public List<IWorker> SearchWorker(string name)//fler sökfunktionen
        {
            List<IWorker> workers = new List<IWorker>();
            //implementera serch

            return workers;
        }


        // search by id
        public IWorker SearchWorker(int id)
        {
            // implementera serch 
            // try get value gör att det blir lite smidigare att kolla om det finns nått med det id:t
            if (registry.TryGetValue(id, out IWorker worker))
            {
                return worker;
            }
            return null;
        }
        public void CreateBackup()
        { 
            var lines = new List<string>();
            foreach (var item in registry)
            {
                lines.Add($"{item.Value.GetId()};{item.Value.GetName()};{item.Value.GetWorkType()};{item.Value.GetShiftType()};{item.Value.GetWorkShoes()};{item.Value.GetStartDate()}");
            }
            File.WriteAllLines("Backup.csv", lines);
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
        public void TestPrinter()
        {
            foreach (var item in registry)
            {
                Console.WriteLine(item.Value.ToString());
            }
        }
        public WorkerRegistry()
        {
            registry = new Dictionary<int, IWorker>();
        }
    }
}
