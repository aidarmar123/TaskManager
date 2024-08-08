using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
    public class Diagram
    {
        
        public SeriesCollection SeriesCollection { get; set; }
        public string Title { get; set; }
        public ChartValues<double> Values { get; set; }
    }
}
