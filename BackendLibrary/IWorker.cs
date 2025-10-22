using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary
{
    internal interface IWorker
    {
        public int GetId();
        public string GetName();
    }
}
