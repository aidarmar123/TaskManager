using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
    public class Processor
    {
        public Processor(string name, string version, string manufacture, UInt32 maxClockSpeed, UInt16 cPUStatus)
        {
            Name = name;
            Version = version;
            Manufacturer = manufacture;
            MaxClockSpeed = maxClockSpeed;
            CPUStatus = cPUStatus;
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public string Manufacturer { get; set; }
        public UInt32 MaxClockSpeed {  get; set; }

        public double CPUStatus {  get; set; }
    }
}
