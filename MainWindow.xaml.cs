using System;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace deck
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, string lpString, int nMaxCount);


        PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        private void pack()
        {
            UpdateRam();
            UpdateCPU();
            UpdateTime();
            getFocusedWin();
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Width = SystemParameters.PrimaryScreenWidth;

            pack();

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        
        private void Timer_Tick(object? sender, EventArgs e)
        {
            pack();
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

        private void UpdateTime()
        {
            DateTime currentTime = DateTime.Now;
            time.Content = currentTime.ToString();
        }

        private void getFocusedWin()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            const int nChars = 256;
            string windowTitle = new string(' ', nChars);
            GetWindowText(foregroundWindow, windowTitle, nChars);
            windowTitle = windowTitle.Trim();
            focusedWindow.Content = windowTitle;
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