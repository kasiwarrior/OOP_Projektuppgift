using BackendLibrary;
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
                Console.WriteLine("2. Sök arbetare");
                Console.WriteLine("3. Uppdatera arbetare");
                Console.WriteLine("4. Ta bort arbetare");
                Console.WriteLine("5. Skapa backup");
                Console.WriteLine("0. Avsluta");
                Console.Write("\nVälj ett alternativ: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("=== Alla arbetare ===\n");
                        workerRegistry.TestPrinter();
                        Pause();
                        break;

                    case "2":
                        SearchWorkerMenu(workerRegistry);
                        break;

                    case "3":
                        UpdateWorkerMenu(workerRegistry);
                        break;

                    case "4":
                        RemoveWorkerMenu(workerRegistry);
                        break;

                    case "5":
                        workerRegistry.CreateBackup();
                        Console.WriteLine("Backup skapad!");
                        Pause();
                        break;

                    case "0":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        Pause();
                        break;
                }
            }
        }

        static void SearchWorkerMenu(WorkerRegistry registry)
        {
            Console.Write("Sök efter ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var worker = registry.SearchWorker(id);
                if (worker != null)
                    Console.WriteLine(worker);
                else
                    Console.WriteLine("Ingen arbetare hittades med det ID:t.");
            }
            else
            {
                Console.WriteLine("Felaktigt ID.");
            }
            Pause();
        }

        static void UpdateWorkerMenu(WorkerRegistry registry)
        {
            Console.Write("Ange ID för arbetare att uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Felaktigt ID.");
                Pause();
                return;
            }

            var worker = registry.SearchWorker(id);
            if (worker == null)
            {
                Console.WriteLine("Ingen arbetare hittades med det ID:t.");
                Pause();
                return;
            }

            Console.WriteLine($"Hittad: {worker}");
            Console.WriteLine("Vad vill du uppdatera?");
            Console.WriteLine("1. Namn");
            Console.WriteLine("2. Skift");
            Console.WriteLine("3. Skyddsskor");
            Console.WriteLine("4. Jobbtyp");
            Console.Write("Val: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Nytt namn: ");
                    string newName = Console.ReadLine();
                    registry.UpdateWorkerName(id, newName);
                    Console.WriteLine("Namn uppdaterat!");
                    break;

                case "2":
                    Console.Write("Nytt skift (Day, Evening, Night): ");
                    if (Enum.TryParse(Console.ReadLine(), true, out ShiftType shift))
                    {
                        registry.UpdateWorkerShift(id, shift);
                        Console.WriteLine("Skift uppdaterat!");
                    }
                    else Console.WriteLine("Felaktigt skift.");
                    break;

                case "3":
                    Console.Write("Har skyddsskor (ja/nej): ");
                    string shoeInput = Console.ReadLine().Trim().ToLower();
                    bool hasShoes = (shoeInput == "ja" || shoeInput == "yes");
                    registry.UpdateWorkerShoes(id, hasShoes);
                    Console.WriteLine("Skyddsskor uppdaterade!");
                    break;

                case "4":
                    Console.Write("Ny jobbtyp (Ant, Soldier, Queen osv.): ");
                    if (Enum.TryParse(Console.ReadLine(), true, out WorkType work))
                    {
                        registry.UpdateWorkerType(id, work);
                        Console.WriteLine("Jobbtyp uppdaterad!");
                    }
                    else Console.WriteLine("Felaktig typ.");
                    break;

                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }

            Pause();
        }

        static void RemoveWorkerMenu(WorkerRegistry registry)
        {
            Console.Write("Ange ett eller flera ID (kommaseparerade): ");
            string input = Console.ReadLine();
            string[] parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            List<int> idsToRemove = new List<int>();
            foreach (string part in parts)
            {
                if (int.TryParse(part.Trim(), out int id))
                    idsToRemove.Add(id);
            }

            int removed = 0;
            foreach (int id in idsToRemove)
            {
                if (registry.RemoveWorker(id))
                    removed++;
            }

            Console.WriteLine($"✅ {removed} arbetare togs bort.");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
    }
}