/*============================================================
// Made by: Elliot Hongyun Lee
// Undergrad Research Project
============================================================*/

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
    public partial class AttPIDMonitor : Window
    {
        private bool _pidXStatus = false;
        private bool _pidYStatus = false;
        private bool _pidZStatus = false;
        internal MainController _mainCtrl;
        static private AttPIDMonitor _instance;
        string[] k = new string[9]; // in order of kp, ki, kd
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

        private AttPIDMonitor()
        {
            InitializeComponent();
            Closed += new System.EventHandler(MyWindow_Closed);
        }
        void MyWindow_Closed(object sender, System.EventArgs e)
        {
            _instance = null;
        }

        static public AttPIDMonitor Instance()
        {
            if (_instance == null) _instance = new AttPIDMonitor();
            return _instance;
        }

        internal MainController MainCtrl
        {
            set { this._mainCtrl = value; ; }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.requestAttPidOnOffStatus();
            
        }

        private void btnXonoff_Click(object sender, RoutedEventArgs e)
        {
            sendAttPidOnOff("x");
        }

        private void btnYonoff_Click(object sender, RoutedEventArgs e)
        {
            sendAttPidOnOff("y");
        }

        private void btnZonoff_Click(object sender, RoutedEventArgs e)
        {

            sendAttPidOnOff("z");
        }

        private void sendAttPidOnOff(string val)
        {
            int x, y, z;

            if (pidXStatus) x = 1;
            else x = 0;
            if (pidYStatus) y = 1;
            else y = 0;
            if (pidZStatus) z = 1;
            else z = 0;

            Console.WriteLine("BEFORE bin: " + x.ToString() + y.ToString() + z.ToString());

            if (val.Equals("x")) x = binInverse(x) & 1;
            if (val.Equals("y")) y = binInverse(y) & 1;
            if (val.Equals("z")) z = binInverse(z) & 1;

            Console.WriteLine("bin: " + x.ToString() + y.ToString() + z.ToString());

            int newNumber = (x & 1) << 1;
            newNumber = (newNumber | (y & 1)) << 1;
            newNumber = (newNumber | (z & 1));
            string msg = newNumber.ToString();

            this._mainCtrl.requestInfo("o" + msg);


            Console.WriteLine("pid on off sent: " + msg);

        }

        private int binInverse(int val)
        {
            int result=-1;
            if (val == 1) result = 0;
            else if (val == 0) result = 1;
            return result;

        }

        private void btnSetX_Click(object sender, RoutedEventArgs e)
        {
            k[0] = this.txtXKp.Text;
            k[1] = this.txtXKd.Text;
            k[2] = this.txtXKi.Text;
            k[3] = this.txtYKp.Text;
            k[4] = this.txtYKd.Text;
            k[5] = this.txtYKi.Text;
            k[6] = this.txtZKp.Text;
            k[7] = this.txtZKd.Text;
            k[8] = this.txtZKi.Text;

            this._mainCtrl.updateAttPidAttConst(k);
        }

        private void btnSetY_Click(object sender, RoutedEventArgs e)
        {
            k[0] = this.txtXKp.Text;
            k[1] = this.txtXKd.Text;
            k[2] = this.txtXKi.Text;
            k[3] = this.txtYKp.Text;
            k[4] = this.txtYKd.Text;
            k[5] = this.txtYKi.Text;
            k[6] = this.txtZKp.Text;
            k[7] = this.txtZKd.Text;
            k[8] = this.txtZKi.Text;

            this._mainCtrl.updateAttPidAttConst(k);
        }

        private void btnSetZ_Click(object sender, RoutedEventArgs e)
        {
            k[0] = this.txtXKp.Text;
            k[1] = this.txtXKd.Text;
            k[2] = this.txtXKi.Text;
            k[3] = this.txtYKp.Text;
            k[4] = this.txtYKd.Text;
            k[5] = this.txtYKi.Text;
            k[6] = this.txtZKp.Text;
            k[7] = this.txtZKd.Text;
            k[8] = this.txtZKi.Text;

            this._mainCtrl.updateAttPidAttConst(k);
        }

        private void btnUpDateConst_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.requestAttPidConst();
        }

      
    }
}
