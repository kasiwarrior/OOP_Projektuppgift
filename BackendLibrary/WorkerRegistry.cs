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
        public List<IWorker> GetAllWorkers()
        {
            return registry.Values.ToList();
        }
        public bool AddWorker(int id,
            string name,
            WorkType workerType,
            ShiftType shiftType,
            bool workShoes,
            DateTime startDate)
        {
            return registry.TryAdd(id, CreateWorker(id, name, workerType, shiftType,workShoes,startDate));
            
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
            int? id = null,
            string? name = null,
            WorkType? workType = null,
            ShiftType? shiftType = null,
            bool? workShoes = null,
            DateTime? startDate = null,
            TimeSortOptions option = TimeSortOptions.Specified
            )
        {
            var query = registry.AsQueryable();

            if (id != null)
            {
                List<IWorker> idWorker = new List<IWorker>();
                idWorker.Add(registry[id.Value]);
                return idWorker;
            }
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
        public void CreateBackup(string backupName)
        {
            // Define the backup folder path (e.g., "Backups/backupName")
            string backupFolder = Path.Combine("Backups", backupName);
            Directory.CreateDirectory(backupFolder); // Ensure folder exists

            // Generate backup filename based on current date/time
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string backupFilePath = Path.Combine(backupFolder, $"{timestamp}.csv");

            // Define log file path
            string logFilePath = Path.Combine(backupFolder, "BackupLog.txt");

            // Collect registry data lines
            var lines = new List<string>();
            foreach (var item in registry)
            {
                lines.Add($"{item.Value.GetId()};{item.Value.GetName()};{item.Value.GetWorkType()};{item.Value.GetShiftType()};{item.Value.GetWorkShoes()};{item.Value.GetStartDate()}");
            }

            // Write the backup file
            File.WriteAllLines(backupFilePath, lines);

            // Log the creation
            File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Created backup: {Path.GetFileName(backupFilePath)}{Environment.NewLine}");

            // Manage backup count (keep only 5 most recent backups)
            var existingBackups = new DirectoryInfo(backupFolder)
                .GetFiles("*.csv")
                .OrderBy(f => f.CreationTime)
                .ToList();

            if (existingBackups.Count > 5)
            {
                int toDelete = existingBackups.Count - 5;
                foreach (var oldFile in existingBackups.Take(toDelete))
                {
                    // Log deletion before removing
                    File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Deleted old backup: {oldFile.Name}{Environment.NewLine}");
                    oldFile.Delete();
                }
            }
        }
        public DateTime? GetNewestBackupDate(string backupName)
        {
            // Define the backup folder path
            string backupFolder = Path.Combine("Backups", backupName);

            // Check if the folder exists and has any backups
            if (!Directory.Exists(backupFolder))
                return null;

            var newestBackup = new DirectoryInfo(backupFolder)
                .GetFiles("*.csv")
                .OrderByDescending(f => f.CreationTime)
                .FirstOrDefault();

            // Return the creation date, or null if no file found
            return newestBackup?.CreationTime;
        }
        public bool LoadBackup(string backupName)
        {
            // Define the backup folder path
            string backupFolder = Path.Combine("Backups", backupName);

            // Validate folder existence
            if (!Directory.Exists(backupFolder))
                return false;

            // Find the newest backup file
            var newestBackup = new DirectoryInfo(backupFolder)
                .GetFiles("*.csv")
                .OrderByDescending(f => f.CreationTime)
                .FirstOrDefault();

            if (newestBackup == null)
                return false;

            try
            {
                // Read and load registry data
                registry = new Dictionary<int, IWorker>();
                string[] lines = File.ReadAllLines(newestBackup.FullName);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length < 6)
                        continue; // skip malformed lines

                    int id = int.Parse(parts[0]);
                    string name = parts[1];
                    WorkType workType = (WorkType)Enum.Parse(typeof(WorkType), parts[2]);
                    ShiftType shiftType = (ShiftType)Enum.Parse(typeof(ShiftType), parts[3]);
                    bool workShoes = bool.Parse(parts[4]);
                    DateTime startDate = DateTime.Parse(parts[5]);

                    // TODO: Replace "Ant" with the correct IWorker implementation
                    AddWorker(id, name, workType, shiftType, workShoes, startDate);
                }

                // Log the successful load
                string logFilePath = Path.Combine(backupFolder, "BackupLog.txt");
                File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Loaded backup: {newestBackup.Name}{Environment.NewLine}");

                return true;
            }
            catch
            {
                // If something goes wrong (file missing, parse error, etc.), fail gracefully
                return false;
            }
        }
        private static IWorker CreateWorker
            (
            int id,
            string name,
            WorkType workerType,
            ShiftType shiftType,
            bool workShoes,
            DateTime startDate
            )
        {
            switch (workerType)
            {
                case WorkType.Ant:
                    return new Ant(id, name, workerType, shiftType, workShoes, startDate);

                case WorkType.Bee:
                    return new Bee(id, name, workerType, shiftType, workShoes, startDate);

                default:
                    throw new ArgumentException($"Okänd Arbetstyp: {workerType}");
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
