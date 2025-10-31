using BackendLibrary;
using System.Runtime.InteropServices;
namespace Interface
{

     internal class Program
    {
        static void Main(string[] args)
        {
            WorkerRegistry workerRegistry = new WorkerRegistry();
            workerRegistry.LoadBackup();

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== 🐜 MYR-KONTORET 🐜 ===");
                Console.WriteLine("1. Visa alla arbetare");
                Console.WriteLine("2. Lägg till arbetare");
                Console.WriteLine("3. Sök arbetare");
                Console.WriteLine("4. Uppdatera arbetare");
                Console.WriteLine("5. Ta bort arbetare");
                Console.WriteLine("6. Skapa backup");
                Console.WriteLine("0. Avsluta");
                Console.Write("\nVälj ett alternativ: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("=== Alla arbetare ===\n");
                        workerRegistry.TestPrinter();
                        WorkerMenu.Pause();
                        break;
                    case "2":
                        WorkerMenu.AddWorkerMenu(workerRegistry);
                        break;

                    case "3":
                        var filterdWorkers = workerRegistry.SearchWorker();
                        Console.WriteLine("Sök: ");
                        Console.ReadLine();
                        if (filterdWorkers.Count == 0)
                        {
                            Console.WriteLine("Inga arbetare hittades.");
                        }
                        else
                        {
                            Console.WriteLine($"Hittade {filterdWorkers.Count} arbetare:\n");

                            foreach (var worker in filterdWorkers)
                            {
                                // Här antar vi att IWorker har metoder för att hämta info
                                Console.WriteLine($"ID: {worker.GetId()}");
                                Console.WriteLine($"Namn: {worker.GetName()}");
                                Console.WriteLine($"Typ: {worker.GetWorkType()}");
                                Console.WriteLine($"Skift: {worker.GetShiftType()}");
                                Console.WriteLine($"Skyddsskor: {(worker.GetWorkShoes() ? "Ja" : "Nej")}");
                                Console.WriteLine($"Startdatum: {worker.GetStartDate():yyyy-MM-dd}");
                                Console.WriteLine("--------------------------------------");
                            }
                        }
                        Console.ReadLine();
                        //WorkerMenu.SearchWorkerMenu(workerRegistry);
                        break;

                    case "4":
                        WorkerMenu.UpdateWorkerMenu(workerRegistry);
                        break;

                    case "5":
                        WorkerMenu.RemoveWorkerMenu(workerRegistry);
                        break;

                    case "6":
                        workerRegistry.CreateBackup();
                        Console.WriteLine("Backup skapad!");
                        WorkerMenu.Pause();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        WorkerMenu.Pause();
                        break;
                }
            }
        }

        
    }
}