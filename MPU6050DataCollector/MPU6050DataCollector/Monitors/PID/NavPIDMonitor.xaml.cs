using MPU6050DataCollector.Controllers;
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
    /// Interaction logic for NavPIDMonitor.xaml
    /// </summary>
    public partial class NavPIDMonitor : Window
    {
        private bool _pidXStatus = false;
        private bool _pidYStatus = false;
        private bool _pidZStatus = false;
        string[] k = new string[11]; // in order of kp, ki, kd


        static private NavPIDMonitor _instance;
        private MainController _mainCtrl;

        static public NavPIDMonitor Instance()
        {
            if (_instance == null) _instance = new NavPIDMonitor();
            return _instance;
        }
        public bool pidXStatus
        {
            set { this._pidXStatus = value;}
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
        
        internal MainController MainCtrl
        {
            set { _mainCtrl = value; }
        }

        public NavPIDMonitor()
        {
            InitializeComponent();
            Closed += new System.EventHandler(MyWindow_Closed);

        }

        void MyWindow_Closed(object sender, System.EventArgs e)
        {
            _instance = null;
        }

        

        private void sendPIDConstants()
        {
            k[0] = this.txtKpPitch.Text;
            k[1] = this.txtKdPitch.Text;
            k[2] = this.txtKiPitch.Text;
            k[3] = this.txtKpRoll.Text;
            k[4] = this.txtKdRoll.Text;
            k[5] = this.txtKiRoll.Text;
            k[6] = this.txtKpHeightAsc.Text;
            k[7] = this.txtKpHeightDesc.Text;
            k[8] = this.txtKdHeightAsc.Text;
            k[9] = this.txtKdHeightDesc.Text;
            k[10] = this.txtKiHeight.Text;
            this._mainCtrl.updateNavPidAttConst(k);

        }
        private void btnSetPitch_Click(object sender, RoutedEventArgs e)
        {
            sendPIDConstants();
        }
        private void btnSetRoll_Click(object sender, RoutedEventArgs e)
        {
            sendPIDConstants();
        }

        private void btnSetHeight_Click(object sender, RoutedEventArgs e)
        {
            sendPIDConstants();
        }

        private void btnNavOnOffRequest_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.requestNavPidOnOffStatus();
        }

        private void btnRequestConstants_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.requestNavPidConst();
        }

        private void sendNavPidOnOff(string val)
        {
            int x, y, z;

            if (_pidXStatus) x = 1;
            else x = 0;
            if (_pidYStatus) y = 1;
            else y = 0;
            if (_pidZStatus) z = 1;
            else z = 0;

            //Console.WriteLine("BEFORE bin: " + x.ToString() + y.ToString() + z.ToString());

            if (val.Equals("x")) x = (~x) & 1;
            if (val.Equals("y")) y = (~y) & 1;
            if (val.Equals("z")) z = (~z) & 1;

           // Console.WriteLine("bin: " + x.ToString() + y.ToString() + z.ToString());

            int newNumber = (x & 1) << 1;
            newNumber = (newNumber | (y & 1)) << 1;
            newNumber = (newNumber | (z & 1));
            string msg = newNumber.ToString();

            this._mainCtrl.requestInfo("O" + msg);
            

        }

        private void btnXonoff_Click(object sender, RoutedEventArgs e)
        {
            sendNavPidOnOff("x");
        }

        private void btnYonoff_Click(object sender, RoutedEventArgs e)
        {
            sendNavPidOnOff("y");
        }

        private void btnZonoff_Click(object sender, RoutedEventArgs e)
        {
            sendNavPidOnOff("z");
        }

        
    }
}
