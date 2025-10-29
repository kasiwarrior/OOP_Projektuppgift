using BackendLibrary;
namespace Interface
{

    internal class Program
    {
        static void Main(string[] args)
        {
            WorkerRegistry workerRegistry = new WorkerRegistry();

            Ant temp = new Ant(1, "Isak");
            workerRegistry.AddWorker(temp.GetId(), temp);
            temp = new Ant(2, "Isak2");
            workerRegistry.AddWorker(temp.GetId(), temp);
            temp = new Ant(3, "Isak3");
            workerRegistry.AddWorker(temp.GetId(), temp);
            temp = new Ant(4, "Isak4");
            workerRegistry.AddWorker(temp.GetId(), temp);
            
            

            workerRegistry.CreateBackup();

            workerRegistry.LoadBackup();
            Console.WriteLine("Done");
            //workerRegistry.CreateBackup();
            //workerRegistry.LoadBackup();
            workerRegistry.TestPrinter();
        }
    }
}
