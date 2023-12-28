using System;
using System.Windows;
using System.Diagnostics;

namespace deck
{
    public partial class MainWindow : Window
    {
        PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public MainWindow()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth;

            UpdateRam();
            UpdateCPU();

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }
        
        private void Timer_Tick(object? sender, EventArgs e)
        {
            UpdateRam();
            UpdateCPU();
        }

        private void UpdateRam()
        {
            Process process = Process.GetCurrentProcess();
            long ramUsage = process.WorkingSet64;
            double ramUsageMB = ramUsage/(1024.0*1024.0);
            ramUsageLabel.Content = $"RAM: {ramUsageMB:F2} MiB";
        }

        private void UpdateCPU()
        {
            Thread.Sleep(100);
            float cpuUsage = cpuCounter.NextValue();
            Thread.Sleep(100);
            cpuUsage = (float)Math.Round(cpuUsage,2);
            cpuUsageLabel.Content = $"CPU: {cpuUsage}%";
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