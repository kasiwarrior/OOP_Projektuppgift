using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary
{
    public interface IWorker
    {
        public int GetId();
        public string GetName();        
        public string GetWorkType();
        public string GetShiftType();  
        // Anställningsdatum
    }
}
