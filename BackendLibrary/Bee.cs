using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary
{
    public class Bee : IWorker
    {
        private int id;
        private string name;
        private WorkType workType;
        private ShiftType shiftType;
        private DateTime startDate;
        private bool workShoes;


        public Bee(int id, string name, WorkType workType, ShiftType shiftType, bool workShoes, DateTime startDate)
        {
            this.id = id;
            this.name = name;
            this.workType = workType;
            this.shiftType = shiftType;
            this.startDate = startDate;
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
            return $"Id: {id}, Namn: {name}, Arbetstyp: Bi, Arbetsskor {workShoes}, Skift: {shiftType}, Anställningsdatum: {startDate:yyyy-MM-dd}";
        }




    }
}
