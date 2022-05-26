using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Precision.Services;

namespace Precision
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void OnCloseButtonClick (object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnMinimizeButtonClick (object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnMaximizeButtonClick (object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
            }
            else if (WindowState == WindowState.Normal)
            {
                this.ResizeMode = ResizeMode.NoResize;
                WindowState = WindowState.Maximized;

            }
        }
    }
}