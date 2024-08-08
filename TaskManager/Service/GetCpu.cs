using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Service
{
    public static class GetCpu
    {
        public static double GetCpuUsage(Process process)
        {
            try
            {
                using (var counter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName))
                {
                    counter.NextValue();
                    System.Threading.Thread.Sleep(100);
                    return counter.NextValue() / 100;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
