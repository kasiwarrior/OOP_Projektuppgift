using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary
{
    public class Ant : IWorker
    {
        private int id;
        private string name;
        public int GetId()
        {
            return id;
        }
        public string GetName()
        {
            return name;
        }
        public override string ToString()
        {
            return $"id: {id} name: {name}";
        }
        public Ant(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
