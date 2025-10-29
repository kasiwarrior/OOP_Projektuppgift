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
        private DateTime startDate;
        private bool workShoes;

        public Ant(int id, string name, WorkType workType, ShiftType shiftType, bool workShoes = true)
        {
            this.id = id;
            this.name = name;
            this.workType = workType;
            this.shiftType = shiftType;
            startDate = DateTime.Now;
            this.workShoes = workShoes;
        }

        public int GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public bool GetWorkShoes()
        {
            return workShoes;
        }

        public bool SetWorkShoes(bool hasShoes)
        {
            workShoes = hasShoes;
            return hasShoes;
        }

        public WorkType GetWorkType()
        {
            return workType;
        }
        
        public ShiftType GetShiftType() 
        {
            return shiftType;    
        }

        public DateTime GetStartDate()
        {
            return startDate;
        }


        public override string ToString()
        {
            return $"Id: {id}, Name: {name}, Worktype: {workType}, WorkShoes {workShoes}, Shift: {shiftType}, StartDate: {startDate}";
        }

        
    }
}
