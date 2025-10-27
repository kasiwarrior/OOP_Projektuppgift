using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary
{
    public class TimeManagement : ITimeManagement
    {
        public DateTime LastUpdated { get; private set; }

        public TimeManagement()
        {
            LastUpdated = DateTime.Now;
        }

        public void UpdateTime()
        {
            LastUpdated = DateTime.Now;
            Console.WriteLine($"[TID] Uppdaterad: {LastUpdated}");
        }
    }
}
