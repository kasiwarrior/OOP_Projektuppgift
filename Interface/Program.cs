using BackendLibrary;
using System.Runtime.InteropServices;
namespace Interface
{

    internal class Program
    {
        static void Main(string[] args)
        {
            WorkerRegistry workerRegistry = new WorkerRegistry();

            Ant temp = new Ant(10238, "Ann-Sophie Gunnarson", WorkType.Ant, ShiftType.Night, true, DateTime.Now);
            workerRegistry.AddWorker(temp.GetId(), temp);
            //temp = new Ant(2, "Isak2");
            //workerRegistry.AddWorker(temp.GetId(), temp);
            //temp = new Ant(3, "Isak3");
            //workerRegistry.AddWorker(temp.GetId(), temp);
            //temp = new Ant(4, "Isak4");
            //workerRegistry.AddWorker(temp.GetId(), temp);

            //workerRegistry.CreateBackup();

            //Console.WriteLine(temp);
            workerRegistry.LoadBackup();
            //workerRegistry.TestPrinter();
            //Console.WriteLine("Done");
            //workerRegistry.CreateBackup();
            //workerRegistry.LoadBackup();
            //workerRegistry.TestPrinter();
            //workerRegistry.PrintLastUpdated();
            List<IWorker> workers = new List<IWorker>();

            // sök exempel
            // Det går att söka på vad som helst av det som finns i IWorker genom att skicka in det man vill söka på i SerchWorker enligt exmplet. 
            //workers = workerRegistry.SearchWorker(startDate: DateTime.Parse("2025 10 26"), option: TimeSortOptions.After);
            //workers = workerRegistry.SearchWorker(startDate: DateTime.Parse("2025 10 26"), option: TimeSortOptions.Before);
            workers = workerRegistry.SearchWorker(startDate: DateTime.Parse("2025 10 26"), option: TimeSortOptions.Specified);

            Console.WriteLine("Sök resultat");
            foreach (var item in workers)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("Test klart!");
            Console.ReadKey();
        }
    }
}
