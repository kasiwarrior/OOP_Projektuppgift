using BackendLibrary;
namespace Interface
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, ALLA! Nej" );

            Dictionary<int, IWorker> WorkerRegestry = new Dictionary<int, IWorker>();

            Ant temp = new Ant(1, "Isak");

            WorkerRegestry.Add(temp.GetId(), temp);

            Console.WriteLine(WorkerRegestry[1].ToString());
        }
    }
}
