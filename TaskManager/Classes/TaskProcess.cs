using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
    public class TaskProcess
    {
        public TaskProcess(int id, string name, long memoryBite)
        {
            Id = id;
            Name = name;
            MemoryBite = memoryBite;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public long MemoryBite { get; set; }
        public long MemoryMB
        {
            get
            {
                return MemoryBite / (1024 * 1024);
            }
        }
        public double CPU { get; set; }
    }
}
