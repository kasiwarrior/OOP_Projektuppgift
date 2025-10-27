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
        private bool workShoes;
        public int GetId() => id;   
        public string GetName() => name;
        public bool GetWorkShoes() => workShoes;
        public bool SetWorkShoes(bool hasShoes) => workShoes = hasShoes;
        public override string ToString()
        {
            return $"Id:{id}, Name:{name}";
        }
        public Ant(int id, string name, bool workShoes = true)
        {
            this.id = id;
            this.name = name;
            this.workShoes = workShoes;
        }
    }
}
