using System;
using System.Windows;
using System.Diagnostics;

namespace deck
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth;

            UpdateRam();

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateRam();
        }

        private void UpdateRam()
        {
            Process process = Process.GetCurrentProcess();
            long ramUsage = process.WorkingSet64;
            double ramUsageMB = ramUsage/(1024.0*1024.0);
            ramUsageLabel.Content = $"RAM: {ramUsageMB:F2} MiB";
        }
        
        private void WindowCentered(object sender, EventArgs e)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double formWidth = this.ActualWidth;
            double formHeight = this.ActualHeight;

            double x = (screenWidth - formWidth) / 2;
            double y = SystemParameters.PrimaryScreenHeight - formHeight;

            this.Left = x;
            this.Top = y;
        }
    }
}