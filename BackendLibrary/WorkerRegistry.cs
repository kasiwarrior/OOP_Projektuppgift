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

            registry.Remove(id);

            return true;
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
            
            Console.WriteLine("Ingen myra hittades med det ID:T");
            return null;
            
        }
        //public bool UpdateWorker(int id)
        //{
        //    SerchWorker(id);
        //    return true;
        //}
        public void CreateBackup()
        { 
            //kanske nått medelande om lyckas eller misslyckas :/
            //lägg till klockslags grejen :) 
            var lines = new List<string>();
            foreach (var item in registry)
            {
                lines.Add($"{item.Value.GetId()};{item.Value.GetName()}");
            }
            File.WriteAllLines("Backup.csv", lines);
        }
        public void LoadBackup()
        {
            //kanske nått medelande om lyckas eller misslyckas :/
            //lägg till klockslags grejen :) 
            if (File.Exists("Backup.csv"))
            {
                registry = new Dictionary<int, IWorker>();
                string[] lines = File.ReadAllLines("Backup.csv");

                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    AddWorker(int.Parse(parts[0]), new Ant(int.Parse(parts[0]), parts[1])); //Fixa så att det inte bara är myror som läggs till
                }
            }
        }
        public void TestPrinter()
        {
            foreach (var item in registry)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public WorkerRegistry()
        {
            registry = new Dictionary<int, IWorker>();
        }
    }
}
