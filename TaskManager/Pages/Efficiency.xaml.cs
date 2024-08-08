using LiveCharts.Wpf;
using LiveCharts;
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
using System.Windows.Threading;

namespace TaskManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для Efficiency.xaml
    /// </summary>
    public partial class Efficiency : Page
    {
        //Таймер
        private DispatcherTimer timer;
       

        List<Diagram> diagrams = new List<Diagram>
        {
            new Diagram
            {
                Title="Cpu",
                Values = new ChartValues<double>()
            },
            new Diagram
            {
                Title = "Memory",
                Values = new ChartValues<double>()
            },
           new Diagram
           {
               Title="GPU",
               Values = new ChartValues<double>()
           }
        };

        
        double contextCpu;
        double contextGPU;
        double contextMemoryKB;
        SeriesCollection contextSeries;

        public Efficiency()
        {
            InitializeComponent();

            Refresh();
            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await Refresh();
        }
        private async Task Refresh()
        {
            

            //Данные памяти
            ManagementObjectSearcher searcherMemory = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach(ManagementObject obj in searcherMemory.Get())
            {
                contextMemoryKB = (ulong)obj["TotalVisibleMemorySize"] - (ulong)obj["FreePhysicalMemory"];
                break;
            }

            //Данные CPU
            ManagementObjectSearcher searcherProcessor = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject obj in searcherProcessor.Get())
            {
                contextCpu = (ushort)obj["CpuStatus"];
                break;
            }

            //Данные GPU
            ManagementObjectSearcher searcherGPU = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            foreach (ManagementObject obj in searcherGPU.Get())
            {
                contextGPU = (uint)obj["CurrentBitsPerPixel"];
                break;
            }

            //Создание диаграммм

            //CPU
            RefreshDiagram(diagrams[0], contextCpu);

            //Память
            RefreshDiagram(diagrams[1], contextMemoryKB/(1024*1024)); //Kb -> Gb

            //GPU
            RefreshDiagram(diagrams[2], contextGPU);

            LVDiagrams.ItemsSource = diagrams;

        }
        private void RefreshDiagram(Diagram diagram, double value)
        {
            if(diagram.Values.Count >= 30)
                diagram.Values.RemoveAt(0);
            diagram.Values.Add(value);
            diagram.SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {

                    Values = diagram.Values,
                    Title =diagram.Title,

                    PointGeometry=null
                }
            };
        }

        private void LVDiagrams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVDiagrams.SelectedItem is Diagram diagram)
            {
               
                contextSeries = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = diagram.Title,
                        Values = diagram.Values,
                        PointGeometry=null

                    }
                };
                PrimaryDiagram.Series = contextSeries;

            }
        }
    }
}
