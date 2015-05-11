using MPU6050DataCollector.Controllers;
using SerialMonitorTest03.ControllerFolder;
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
using System.Windows.Shapes;

namespace MPU6050DataCollector
{
    /// <summary>
    /// Interaction logic for PIDMonitor.xaml
    /// </summary>
    public partial class PIDMonitor : Window
    {
        internal MainController _mainCtrl;
        internal PIDMonitor(MainController x)
        {
            this._mainCtrl = x;
            InitializeComponent();
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.requestPidOnOffStatus();
            this._mainCtrl.requestPidConst();
        }

        private void btnXonoff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnYonoff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnZonoff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSetX_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.updatePidConstX();
        }

        private void btnSetY_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.updatePidConstY();
        }

        private void btnSetZ_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.updatePidConstZ();
        }

      

      
    }
}
