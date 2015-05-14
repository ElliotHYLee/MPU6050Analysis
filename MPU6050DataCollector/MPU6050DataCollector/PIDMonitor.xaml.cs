using MPU6050DataCollector.Controllers;
using SerialMonitorTest03.ControllerFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private bool _pidXStatus = false;
        private bool _pidYStatus = false;
        private bool _pidZStatus = false;
        internal MainController _mainCtrl;

        public bool pidXStatus
        {
            set { this._pidXStatus = value; }
            get { return this._pidXStatus; }
        }

        public bool pidYStatus
        {
            set { this._pidYStatus = value; }
            get { return this._pidYStatus; }
        }

        public bool pidZStatus
        {
            set { this._pidZStatus = value; }
            get { return this._pidZStatus; }
        }

        internal PIDMonitor(MainController x)
        {
            this._mainCtrl = x;
            InitializeComponent();
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.requestPidOnOffStatus();
            
        }

        private void btnXonoff_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";
            if (pidXStatus)
            {
                msg = "O10";
            }
            else
            {
                msg = "O11";
            }
            this._mainCtrl.setPidStatusNew(msg);
            Console.WriteLine(msg);
        }

        private void btnYonoff_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";
            if (pidYStatus)
            {
                msg = "O20";
            }
            else
            {
                msg = "O21";
            }

            this._mainCtrl.setPidStatusNew(msg);
            Console.WriteLine(msg);
        }

        private void btnZonoff_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";
            if (pidZStatus)
            {
                msg = "O30";
            }
            else
            {
                msg = "O31";
            }
            this._mainCtrl.setPidStatusNew(msg);
            Console.WriteLine(msg);

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

        private void btnUpDateConst_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.requestPidConst();
        }

      

      
    }
}
