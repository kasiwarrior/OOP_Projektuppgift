using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary
{
    public interface IWorker
    {
        public DateTime GetStartDate();
        public int GetId();
        public string GetName();        
        public WorkType GetWorkType();
        public ShiftType GetShiftType();
        
    }
}
