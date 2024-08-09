using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
    public class User
    {
        public User(string name)
        {
            Name = name;
            
        }

        public string Name { get; set; }
        public List<TaskProcess> ProcessUsers { get; set; }
    }
}
