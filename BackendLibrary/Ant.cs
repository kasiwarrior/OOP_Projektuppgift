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
        private WorkType workType;
        private ShiftType shiftType;
        
        public Ant(int id, string name, WorkType workType, ShiftType shiftType)
        {
            this.id = id;
            this.name = name;
            this.workType = workType;
            this.shiftType = shiftType;  
        }

        public int GetId()
        {
            return id;
        }
        public string GetName()
        {
            return name;
        }
        public string GetWorkType() 
        {
            return workType.ToString();
        }
        public string GetShiftType() 
        {
            return shiftType.ToString();    
        }
       
        public override string ToString()
        {
            return $"Id:{id}, Name:{name}, Worktype: {workType}, Shift: {shiftType}";
        }
       
        

    }
}
