using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
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
using TaskManager.Classes;
using TaskManager.Service;

namespace TaskManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {

        public UsersPage()
        {
            InitializeComponent();
            Refresh();

        }

        private void Refresh()
        {
        //Занимает много времени
            List<User> users = GetProcessesByUser();
            LBUsers.ItemsSource = users;
        }

        public List<User> GetProcessesByUser()
        {
            List<User> users = new List<User>();
            List<TaskProcess> processList = new List<TaskProcess>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Process");
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    string[] args = new string[2];
                    int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", args));
                    string user = args[0];

                    if (returnVal == 0)
                    {
                        int processId = Convert.ToInt32(obj["ProcessId"]);
                        string processName = obj["Name"].ToString();
                        double RAM = (ulong)obj["WorkingSetSize"];

                        var taskProcess = new TaskProcess(processId, processName, RAM)
                        {
                            User = user,
                            CPU = GetCpu.GetCpuUsage(Process.GetProcessById(processId))
                        };
                        processList.Add(taskProcess);

                        if (users.Where(x => x.Name == user).ToList().Count() == 0)
                            users.Add(new User(user));

                    }
                }
            }
            catch
            {

            }

            foreach (User user in users)
            {
                var listProcessCurrentUser = processList.Where(x => x.User == user.Name).ToList();
                user.ProcessUsers = listProcessCurrentUser.OrderByDescending(x => x.MemoryBite).ToList();
            }
            return users;
        }
    }
}
