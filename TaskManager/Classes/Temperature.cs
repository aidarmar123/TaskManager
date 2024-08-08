using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
    public class Temperature
    {
        public Temperature( int maxTemp, int minTemp, string status, UInt32 resolution) 
        {
          
            MaxReadable= maxTemp;
            MinReadable= minTemp;
            Status = status;
            Resolution = resolution;
        }
        public int CurrentTemperature { get; set; }
        public int MaxReadable {  get; set; }
        public int MinReadable {  get; set; }
        public string Status { get; set; }
        public UInt32 Resolution { get; set; }
    }
}
