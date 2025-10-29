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
            registry.Add(id, worker);
            return true; // Add error detection
        }
        public bool RemoveWorker()
        {

            return true;
        }
        public List<IWorker> SearchWorker(string name)//fler sökfunktionen
        {
            List<IWorker> workers = new List<IWorker>();
            //implementera serch

            return workers;
        }
        public IWorker SearchWorker(int id)
        {

            //implementera serch

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

            //Uppdatera tid
            lastBackupTime = DateTime.Now;
            File.WriteAllText("BackupTime.txt", lastBackupTime.ToString());

            Console.WriteLine($"Backup skapad {lastBackupTime}");
        }
        //public void loadbackup()
        //{
        //    //kanske nått medelande om lyckas eller misslyckas :/
        //    //lägg till klockslags grejen :) 
        //    if (file.exists("backup.csv"))
        //    {
        //        registry = new dictionary<int, iworker>();
        //        string[] lines = file.readalllines("backup.csv");

        //        foreach (string line in lines)
        //        {
        //            string[] parts = line.split(';');
        //            addworker(int.parse(parts[0]), new ant(int.parse(parts[0]), parts[1])); //fixa så att det inte bara är myror som läggs till
        //        }
        //    }
        //}

        //Skriver ut senaste Backup
        public void PrintLastUpdated()
        {
            if (lastBackupTime.HasValue)
                Console.WriteLine($"Senaste backup skapades: {lastBackupTime}");
            else
                Console.WriteLine("Ingen backup har skapats än.");
        }
        public void TestPrinter()
        {
            foreach (var item in registry)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
