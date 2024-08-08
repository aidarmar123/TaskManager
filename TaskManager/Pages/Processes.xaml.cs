using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TaskManager.Classes;
using TaskManager.Service;

namespace TaskManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для Processes.xaml
    /// </summary>
    public partial class Processes : Page
    {
       

        List<TaskProcess> processes;
        Process contextProcess;
        public Processes()
        {
            InitializeComponent();
            Refreshs();
            
        }

        private void KillTask_Click(object sender, RoutedEventArgs e)
        {
            contextProcess.Kill();
            contextProcess = null;
            Refreshs();
            KillTask.IsEnabled = false;
        }

        private void DGTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGTasks.SelectedItem is TaskProcess process)
            {
                contextProcess = Process.GetProcesses().FirstOrDefault(x => x.Id == process.Id);
                if (contextProcess != null)
                    KillTask.IsEnabled = true;

            }

        }
        private void Refreshs()
        {
            processes = new List<TaskProcess>();

            //Данные всех процессов 
            foreach (Process process in Process.GetProcesses())
            {
                TaskProcess taskProcess = new TaskProcess(process.Id, process.ProcessName, process.WorkingSet64);
                taskProcess.CPU = GetCpu.GetCpuUsage(process);
                processes.Add(taskProcess);
            }
            DGTasks.ItemsSource = processes.OrderByDescending(x => x.MemoryBite).ToList();

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refreshs();
        }
    }
}
