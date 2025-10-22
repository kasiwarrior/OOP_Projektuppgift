using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary
{
    internal class Ant : IWorker
    {
        public int GetId()
        {
            return 1;
        }
        public string GetName()
        {
            return "name";
        }
    }
}
