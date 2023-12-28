using System;
using System.Windows;

namespace deck
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowCentered(object sender, EventArgs e)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double formWidth = this.ActualWidth;

            double x = (screenWidth - formWidth) / 2;
            double y = 0;

            this.Left = x;
            this.Top = y;
        }
    }
}
