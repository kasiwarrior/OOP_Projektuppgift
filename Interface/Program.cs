using BackendLibrary;
namespace Interface
{

    internal class Program
    {
        static void Main(string[] args)
        {
            WorkerRegistry workerRegistry = new WorkerRegistry();

            

            Ant temp = new Ant(id: 2000, "Myra1", workType: WorkType.Ant, ShiftType.Day, workShoes: true);
            workerRegistry.AddWorker(temp.GetId(), temp);
            temp = new Ant(id: 2, "Myra2", workType: WorkType.Ant, ShiftType.Day, workShoes: true);
            workerRegistry.AddWorker(temp.GetId(), temp);
            temp = new Ant(id: 3, "Myra3", workType: WorkType.Ant, ShiftType.Day, workShoes: true);
            workerRegistry.AddWorker(temp.GetId(), temp);
            
            
            workerRegistry.TestPrinter();
            Console.WriteLine("Tryck för att skriva ut listan igen....");
            Console.ReadKey();
            workerRegistry.UpdateWorkerShift(2000,ShiftType.Night);
            workerRegistry.UpdateWorkerShift(2,ShiftType.Night);
            workerRegistry.UpdateWorkerShift(3,ShiftType.Night);
            
            workerRegistry.UpdateWorkerName(2000, "Fredrik Domert");
            workerRegistry.UpdateWorkerName(2, "Pär Hedström");
            workerRegistry.UpdateWorkerName(3, "Isak Wallin Färje");
            
            workerRegistry.UpdateWorkerShoes(2000, false);
            workerRegistry.UpdateWorkerShoes(2, false);
            workerRegistry.UpdateWorkerShoes(3, false);
            
            Console.WriteLine("Skapa myra");
            
            //Console.WriteLine("Vilken myra vill du avliva?");
            //int antPick = int.Parse(Console.ReadLine());
            //workerRegistry.RemoveWorker(antPick);
            workerRegistry.TestPrinter();
        }
    }
}
