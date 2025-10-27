using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendLibrary
{
    public interface ITimeManagement
    {
        DateTime LastUpdated { get; }
        void UpdateTime();
    }
}
