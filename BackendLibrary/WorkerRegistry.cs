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
    public class WorkerRegistry
    {
        Dictionary<int, IWorker> registry;



        public string AddWorker(int id, IWorker worker)
        {
            registry.Add(id, worker);
            return "Succses"; // Add error detection
        }
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
                    AddWorker(int.Parse(parts[0]), new Ant(int.Parse(parts[0]), parts[1])); //Fixsa så att det inte bara är myror som läggs till
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
