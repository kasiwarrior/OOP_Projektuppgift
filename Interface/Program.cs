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
                        Console.Clear();
                        Console.WriteLine("=== Lägg till ny arbetare ===");

                        try
                        {
                            Console.Write("ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int id))
                            {
                                Console.WriteLine("Felaktigt ID.");
                                WorkerMenu.Pause();
                               
                            }

                            Console.Write("Namn: ");
                            string name = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.WriteLine("Namnet får inte vara tomt.");
                                WorkerMenu.Pause();
                                break;
                            }

                            Console.Write("Jobbtyp (Ant, Bee): ");
                            if (!Enum.TryParse(Console.ReadLine(), true, out WorkType workType))
                            {
                                Console.WriteLine("Felaktig jobbtyp.");
                                WorkerMenu.Pause();
                                break;
                            }

                            Console.Write("Skift (Day, Night): ");
                            if (!Enum.TryParse(Console.ReadLine(), true, out ShiftType shiftType))
                            {
                                Console.WriteLine("Felaktigt skift.");
                                WorkerMenu.Pause();
                                break;
                            }

                            Console.Write("Har skyddsskor (ja/nej): ");
                            string shoeInput = Console.ReadLine().Trim().ToLower();
                            bool hasShoes = (shoeInput == "ja" || shoeInput == "yes");

                            DateTime startDate = DateTime.Now;

                            var newAnt = new Ant(id, name, workType, shiftType, hasShoes, startDate);

                            if (workerRegistry.AddWorker(id, newAnt))
                                Console.WriteLine("Ny arbetare tillagd!");
                            else
                                Console.WriteLine("Kunde inte lägga till – ID finns redan.");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Fel vid inmatning: {ex.Message}");
                        }

                        WorkerMenu.Pause();
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