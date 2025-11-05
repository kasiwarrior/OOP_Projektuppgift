using BackendLibrary;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
namespace Interface
{

    internal class Program
    {
        static void Main(string[] args)
        {
            WorkerRegistry workerRegistry = new WorkerRegistry();
            workerRegistry.LoadBackup("WorkerRegistry");

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== 🐜 MYR-KONTORET 🐜 ===");
                Console.WriteLine("1. Lägg till arbetare");
                Console.WriteLine("2. Visa alla arbetare");
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
                        AddWorkerMenu(workerRegistry);
                        break;

                    case "2":
                        Console.WriteLine("=== Alla arbetare ===\n");
                        workerRegistry.TestPrinter();
                        Pause();
                        break;

                    case "3":
                        SearchWorkerMenu(workerRegistry);
                        break;

                    case "4":
                        UpdateWorkerMenu(workerRegistry);
                        break;

                    case "5":
                        RemoveWorkerMenu(workerRegistry);
                        break;

                    case "6":
                        workerRegistry.CreateBackup("WorkerRegistry");
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
            Console.WriteLine("=== Avancerad sökning ===");
            Console.WriteLine("Tryck Enter för att hoppa över ett fält.");

            // ID search
            Console.Write("ID: ");
            int? id = null;
            string idInput = Console.ReadLine();
            if (int.TryParse(idInput, out int parsedId))
                id = parsedId;

            // Name search
            Console.Write("Namn (del av namn): ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = null;

            // WorkType search
            Console.Write("Jobbtyp (Ant or Bee): ");
            WorkType? workType = null;
            string workTypeInput = Console.ReadLine();
            if (Enum.TryParse(workTypeInput, true, out WorkType wt))
                workType = wt;

            // ShiftType search
            Console.Write("Skift (Day, Evening, Night): ");
            ShiftType? shiftType = null;
            string shiftInput = Console.ReadLine();
            if (Enum.TryParse(shiftInput, true, out ShiftType st))
                shiftType = st;

            // WorkShoes search
            Console.Write("Har skyddsskor? (ja/nej): ");
            bool? workShoes = null;
            string shoesInput = Console.ReadLine()?.Trim().ToLower();
            if (shoesInput == "ja" || shoesInput == "yes")
                workShoes = true;
            else if (shoesInput == "nej" || shoesInput == "no")
                workShoes = false;

            // Start date and comparison option
            Console.Write("Startdatum (åååå-mm-dd) eller lämna tomt: ");
            DateTime? startDate = null;
            string dateInput = Console.ReadLine();
            if (DateTime.TryParse(dateInput, out DateTime dt))
                startDate = dt;

            TimeSortOptions option = TimeSortOptions.Specified;
            if (startDate != null)
            {
                Console.WriteLine("Jämförelsealternativ:");
                Console.WriteLine("1. Samma dag");
                Console.WriteLine("2. Före datumet");
                Console.WriteLine("3. Efter datumet");
                Console.Write("Val: ");
                string opt = Console.ReadLine();
                switch (opt)
                {
                    case "2":
                        option = TimeSortOptions.Before;
                        break;
                    case "3":
                        option = TimeSortOptions.After;
                        break;
                    default:
                        option = TimeSortOptions.Specified;
                        break;
                }
            }


            List<IWorker> results = registry.SearchWorker(
                id,
                name,
                workType,
                shiftType,
                workShoes,
                startDate,
                option
            );

            Console.Clear();
            Console.WriteLine("=== Sökresultat ===\n");

            if (results.Count == 0)
            {
                Console.WriteLine("Inga arbetare matchade sökkriterierna.");
            }
            else
            {
                foreach (var worker in results)
                {
                    Console.WriteLine(worker);
                }
                Console.WriteLine($"\nTotalt: {results.Count} arbetare hittades.");
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

            var worker = registry.SearchWorker(id).First();
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

            Console.WriteLine($"{removed} arbetare togs bort.");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }

        public static void AddWorkerMenu(WorkerRegistry registry)
        {
            Console.Clear();
            Console.WriteLine("=== Lägg till ny arbetare ===");

            try
            {
                Console.Write("ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Felaktigt ID.");
                    Pause();
                    return;
                }

                Console.Write("Namn: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Namnet får inte vara tomt.");
                    Pause();
                    return;
                }

                Console.WriteLine($"\nTillgängliga jobbtyper: {string.Join(", ", Enum.GetNames(typeof(WorkType)))}");
                Console.Write("Jobbtyp: ");
                if (!Enum.TryParse(Console.ReadLine(), true, out WorkType workType))
                {
                    Console.WriteLine("Felaktig jobbtyp.");
                    Pause();
                    return;
                }

                Console.WriteLine($"\nTillgängliga skift: {string.Join(", ", Enum.GetNames(typeof(ShiftType)))}");
                Console.Write("Skift: ");
                if (!Enum.TryParse(Console.ReadLine(), true, out ShiftType shiftType))
                {
                    Console.WriteLine("Felaktigt skift.");
                    Pause();
                    return;
                }

                Console.Write("Har skyddsskor (ja/nej): ");
                string shoeInput = (Console.ReadLine() ?? "").Trim().ToLower();
                bool hasShoes = (shoeInput == "ja" || shoeInput == "yes");

                DateTime startDate = DateTime.Now;



                if (registry.AddWorker(id, name, workType, shiftType, hasShoes, startDate))
                    Console.WriteLine("Ny arbetare tillagd!");
                else
                    Console.WriteLine("Kunde inte lägga till – ID finns redan.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid inmatning: {ex.Message}");
            }

            Pause();
        }

    }



}
