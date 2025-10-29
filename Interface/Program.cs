using BackendLibrary;
namespace Interface
{

    internal class Program
    {
        static void Main(string[] args)
        {
            WorkerRegistry workerRegistry = new WorkerRegistry();

            

            Ant temp = new Ant(id: 55, "Myra1", workType: WorkType.Ant, ShiftType.Day, workShoes: true);
            workerRegistry.AddWorker(temp.GetId(), temp);
            temp = new Ant(id: 2, "Myra2", workType: WorkType.Ant, ShiftType.Day, workShoes: true);
            workerRegistry.AddWorker(temp.GetId(), temp);
            temp = new Ant(id: 3, "Myra3", workType: WorkType.Ant, ShiftType.Day, workShoes: true);
            workerRegistry.AddWorker(temp.GetId(), temp);
            
            
            workerRegistry.TestPrinter();
            Console.WriteLine("Tryck för att skriva ut listan igen....");
            Console.ReadKey();
            workerRegistry.UpdateWorkerShift(55,ShiftType.Night);
            workerRegistry.UpdateWorkerShift(2,ShiftType.Night);
            workerRegistry.UpdateWorkerShift(3,ShiftType.Night);
            
            workerRegistry.UpdateWorkerName(55, "Fredrik Domert");
            workerRegistry.UpdateWorkerName(2, "Pär Hedström");
            workerRegistry.UpdateWorkerName(3, "Isak Wallin Färje");
            
            workerRegistry.UpdateWorkerShoes(55, false);
            workerRegistry.UpdateWorkerShoes(2, false);
            workerRegistry.UpdateWorkerShoes(3, false);
            
            workerRegistry.UpdateWorkerType(55, WorkType.Bee);
            workerRegistry.UpdateWorkerType( 2, WorkType.Bee);
            workerRegistry.UpdateWorkerType(3, WorkType.Bee);
            
            workerRegistry.TestPrinter();
            
            Console.WriteLine("Vilken myra vill du avliva?");
            int antPick = int.Parse(Console.ReadLine());
            workerRegistry.RemoveWorker(antPick);
            
            workerRegistry.TestPrinter();
        }
    }
}
