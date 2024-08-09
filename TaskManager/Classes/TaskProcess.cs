using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
    public class TaskProcess
    {
        public TaskProcess(int id, string name, double memoryBite)
        {
            Id = id;
            Name = name;
            MemoryBite = memoryBite;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public double MemoryBite { get; set; }
        public double MemoryMB
        {
            get
            {
                return MemoryBite / (1024 * 1024);
            }
        }
        public double CPU { get; set; }
    }
}
