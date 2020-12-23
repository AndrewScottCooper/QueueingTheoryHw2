using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Queueing_Theory_Homework_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public TimeManager TimeKeeper; 
        public MainWindow()
        {
            InitializeComponent();
            
        }
        //Series Collection for the bar chart
        public SeriesCollection BarCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
       
        //This is the SeriesCollection for the Line Chart
        public SeriesCollection SeriesCollection { get; set; }

        private void NumOfCTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Init the simulation 
        private void SimStartBtn_Click(object sender, RoutedEventArgs e)
        {
            //Double check everything is input correctly 
            if (CheckInputs())
            {
                //Create the Time Manager and pass the user inputs into it
                TimeKeeper = new TimeManager(int.Parse(TimeTextBox.Text), int.Parse(NumOfCTextBox.Text), 
                    int.Parse(LambdaTextBox.Text), int.Parse(MuTextBox.Text));
                TimeKeeper.TheUnstoppableMarchOfTime();
                UpdateLabels();
                GenerateGraphs();
                
            }
            else
            {
                //stop program from running
                return; 
            }
        }

        private bool CheckInputs()
        {
            return true; 
        }

        //This method sends the simulation results to the graphs
        public void GenerateGraphs()
        { if(BarCollection != null)
            {
                BarCollection = new SeriesCollection();
                SeriesCollection = null;
            }
            BarCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "C M/M/1",
                    //LQ LS WQ WS
                    Values = new ChartValues<float> { TimeKeeper.mm1LQ, TimeKeeper.mm1LS, TimeKeeper.mm1WQ, TimeKeeper.mm1WS }
                }
            };

            //adding series will update and animate the chart automatically
            BarCollection.Add(new ColumnSeries
            {
                Title = "M/M/C",
                Values = new ChartValues<float> { TimeKeeper.mmcLQ, TimeKeeper.mmcLS, TimeKeeper.mmcWQ, TimeKeeper.mmcWS }
            });


            Labels = new[] { "LQ", "LS", "WQ", "WS" };
            Formatter = value => value.ToString("N");


            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "C M/M/1",
                    Values = TimeKeeper.QueueManager.MM1WS.AsChartValues()
                },
                new LineSeries
                {
                    Title = "M/M/C",
                    Values = TimeKeeper.QueueManager.myMMCQueue.WS.AsChartValues(),
                    PointGeometry = null
                }
            };

            DataContext = this;

        }

        public void UpdateLabels()
        {
            MMCNumServedLabel.Content = "MMC Cust. Served: " + TimeKeeper.mmcNumCompleted;
            MM1NumServedLabel.Content = "C MM1 Cust. Served: " + TimeKeeper.mm1NumCompleted;

            WSLabel.Content = "WS MMC: " + TimeKeeper.mmcWS +  " MM1: " + TimeKeeper.mm1WS;
            WQLabel.Content = "WQ MMC: " + TimeKeeper.mmcWQ + " MM1: " + TimeKeeper.mm1WQ;
            LSLabel.Content = "LS MMC: " + TimeKeeper.mmcLS + " MM1: " + TimeKeeper.mm1LS;
            LQLabel.Content = "LQ MMC: " + TimeKeeper.mmcLQ + " MM1: " + TimeKeeper.mm1LQ;
        }
       
    }
}
