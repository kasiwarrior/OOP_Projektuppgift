using BackendLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public class WorkerMenu
    {
        //    public static void SearchWorkerMenu(WorkerRegistry registry)
        //    {
        //        Console.Write("Sök efter ID: ");
        //        if (int.TryParse(Console.ReadLine(), out int id))
        //        {
        //            registry.SearchWorker(id, out var worker);

        //            if (worker != null)
        //                Console.WriteLine(worker);
        //            else
        //                Console.WriteLine("Ingen arbetare hittades med det ID:t.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Felaktigt ID.");
        //        }
        //        Pause();
        //    }

        //    public static void UpdateWorkerMenu(WorkerRegistry registry)
        //    {
        //        Console.Write("Ange ID för arbetare att uppdatera: ");
        //        if (!int.TryParse(Console.ReadLine(), out int id))
        //        {
        //            Console.WriteLine("Felaktigt ID.");
        //            Pause();
        //            return;
        //        }

        //        registry.SearchWorker(id, out var worker);
        //        if (worker == null)
        //        {
        //            Console.WriteLine("Ingen arbetare hittades med det ID:t.");
        //            Pause();
        //            return;
        //        }

        //        Console.WriteLine($"Hittad: {worker}");
        //        Console.WriteLine("Vad vill du uppdatera?");
        //        Console.WriteLine("1. Namn");
        //        Console.WriteLine("2. Skift");
        //        Console.WriteLine("3. Skyddsskor");
        //        Console.WriteLine("4. Jobbtyp");
        //        Console.Write("Val: ");

        //        switch (Console.ReadLine())
        //        {
        //            case "1":
        //                Console.Write("Nytt namn: ");
        //                string newName = Console.ReadLine();
        //                registry.UpdateWorkerName(id, newName);
        //                Console.WriteLine("Namn uppdaterat!");
        //                break;

        //            case "2":
        //                Console.Write("Nytt skift (Day, Evening, Night): ");
        //                if (Enum.TryParse(Console.ReadLine(), true, out ShiftType shift))
        //                {
        //                    registry.UpdateWorkerShift(id, shift);
        //                    Console.WriteLine("Skift uppdaterat!");
        //                }
        //                else Console.WriteLine("Felaktigt skift.");
        //                break;

        //            case "3":
        //                Console.Write("Har skyddsskor (ja/nej): ");
        //                string shoeInput = Console.ReadLine().Trim().ToLower();
        //                bool hasShoes = (shoeInput == "ja" || shoeInput == "yes");
        //                registry.UpdateWorkerShoes(id, hasShoes);
        //                Console.WriteLine("Skyddsskor uppdaterade!");
        //                break;

        //            case "4":
        //                Console.Write("Ny jobbtyp (Ant, Soldier, Queen osv.): ");
        //                if (Enum.TryParse(Console.ReadLine(), true, out WorkType work))
        //                {
        //                    registry.UpdateWorkerType(id, work);
        //                    Console.WriteLine("Jobbtyp uppdaterad!");
        //                }
        //                else Console.WriteLine("Felaktig typ.");
        //                break;

        //            default:
        //                Console.WriteLine("Ogiltigt val.");
        //                break;
        //        }

        //        Pause();
        //    }

        //    public static void RemoveWorkerMenu(WorkerRegistry registry)
        //    {
        //        Console.Write("Ange ett eller flera ID (kommaseparerade): ");
        //        string input = Console.ReadLine();
        //        string[] parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

        //        List<int> idsToRemove = new List<int>();
        //        foreach (string part in parts)
        //        {
        //            if (int.TryParse(part.Trim(), out int id))
        //                idsToRemove.Add(id);
        //        }

        //        int removed = 0;
        //        foreach (int id in idsToRemove)
        //        {
        //            if (registry.RemoveWorker(id))
        //                removed++;
        //        }

        //        Console.WriteLine($"{removed} arbetare togs bort.");
        //        WorkerMenu.Pause();
        //    }


        //    public static void Pause()
        //    {
        //        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        //        Console.ReadKey();
        //    }

        //    public static void AddWorkerMenu(WorkerRegistry registry)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("=== Lägg till ny arbetare ===");

        //        try
        //        {
        //            Console.Write("ID: ");
        //            if (!int.TryParse(Console.ReadLine(), out int id))
        //            {
        //                Console.WriteLine("Felaktigt ID.");
        //                Pause();
        //                return;
        //            }

        //            Console.Write("Namn: ");
        //            string name = Console.ReadLine();
        //            if (string.IsNullOrWhiteSpace(name))
        //            {
        //                Console.WriteLine("Namnet får inte vara tomt.");
        //                Pause();
        //                return;
        //            }

        //            Console.WriteLine($"\nTillgängliga jobbtyper: {string.Join(", ", Enum.GetNames(typeof(WorkType)))}");
        //            Console.Write("Jobbtyp: ");
        //            if (!Enum.TryParse(Console.ReadLine(), true, out WorkType workType))
        //            {
        //                Console.WriteLine("Felaktig jobbtyp.");
        //                Pause();
        //                return;
        //            }

        //            Console.WriteLine($"\nTillgängliga skift: {string.Join(", ", Enum.GetNames(typeof(ShiftType)))}");
        //            Console.Write("Skift: ");
        //            if (!Enum.TryParse(Console.ReadLine(), true, out ShiftType shiftType))
        //            {
        //                Console.WriteLine("Felaktigt skift.");
        //                Pause();
        //                return;
        //            }

        //            Console.Write("Har skyddsskor (ja/nej): ");
        //            string shoeInput = (Console.ReadLine() ?? "").Trim().ToLower();
        //            bool hasShoes = (shoeInput == "ja" || shoeInput == "yes");

        //            DateTime startDate = DateTime.Now;



        //            if (registry.AddWorker(id, name, workType, shiftType, hasShoes, startDate))
        //                Console.WriteLine("Ny arbetare tillagd!");
        //            else
        //                Console.WriteLine("Kunde inte lägga till – ID finns redan.");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Fel vid inmatning: {ex.Message}");
        //        }

        //        Pause();
        //    }

        //}
    }
}
