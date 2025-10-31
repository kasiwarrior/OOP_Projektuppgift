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
                        workerRegistry.TestPrinter();
                        WorkerMenu.Pause();
                        break;
                    case "2":
                        WorkerMenu.AddWorkerMenu(workerRegistry);
                        break;

                    case "3":
                        WorkerMenu.SearchWorkerMenu(workerRegistry);
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