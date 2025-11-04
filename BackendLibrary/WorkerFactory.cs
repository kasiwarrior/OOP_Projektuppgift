using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary
{
    internal class WorkerFactory
    {
        public static IWorker CreateWorker
            (
            WorkType workerType,
            int id,
            string name,
            ShiftType shiftType,
            bool workShoes,
            DateTime startDate
            )
        {
            switch (workerType)
            {
                case WorkType.Ant:
                    return new Ant(id, name, workerType, shiftType, workShoes, startDate);

                case WorkType.Bee:
                    return new Bee(id, name, workerType, shiftType, workShoes, startDate);

                default:
                    throw new ArgumentException($"Okänd Arbetstyp: {workerType}");
            }
        }
    }
}
