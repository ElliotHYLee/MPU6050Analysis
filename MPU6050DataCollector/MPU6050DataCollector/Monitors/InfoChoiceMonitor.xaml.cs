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
    /// Interaction logic for InfoChoiceMonitor.xaml
    /// </summary>
    public partial class InfoChoiceMonitor : Window
    {
        private static InfoChoiceMonitor _instance = null;
        private MainController _mainCtrl;
        private int[] chkList = new int[32];
        private int infoChoice = 0;
        private CheckBox[] chkBoxArr = new CheckBox[32];
        private int numberOfCheckBox = 12;
           

        private InfoChoiceMonitor()
        {
            InitializeComponent();
            Closed += new System.EventHandler(MyWindow_Closed);
            chkBoxArr[0] = chkEuler;
            chkBoxArr[1] = chkGyro;
            chkBoxArr[2] = chkAcc;
            chkBoxArr[3] = chkMag;
            chkBoxArr[4] = chkMotor;
            chkBoxArr[5] = chkThrottle;
            chkBoxArr[6] = chkLocalCoord;
            chkBoxArr[7] = chkDistGround;
            chkBoxArr[8] = chkAttPidOut;
            chkBoxArr[9] = chkNavPidOut;
            chkBoxArr[10] = chkAttCtrlRef;
            chkBoxArr[11] = chkNavCtrlRef;
        }

        internal static InfoChoiceMonitor Instance()
        {
            if (_instance == null) _instance = new InfoChoiceMonitor();
            return _instance;
        }

        void MyWindow_Closed(object sender, System.EventArgs e)
        {
            _instance = null;
        }

        internal MainController MainCtrl
        {
            set
            {
                _mainCtrl = value;
                requestInfoChoice();
            }
        }

        private void btnRequestCurrentStatus_Click(object sender, RoutedEventArgs e)
        {
            requestInfoChoice();
        }

        private async void requestInfoChoice()
        {
            btnRequestCurrentStatus.IsEnabled = false;
            btnSetUp.IsEnabled = false;
            _mainCtrl.requestInfoChoice();
            await Task.Delay(1500);
            infoChoice = Int32.Parse(_mainCtrl.InfoChoice);
            readAndUpdateInfoChoice();
            btnRequestCurrentStatus.IsEnabled = true;
            btnSetUp.IsEnabled = true;
        }


        private void btnSetUp_Click(object sender, RoutedEventArgs e)
        {
            setup();
        }


        private async void setup()
        {
            btnSetUp.IsEnabled = false;
            btnRequestCurrentStatus.IsEnabled = false;
            _mainCtrl.sendInfoChoice(infoChoice.ToString());
            await Task.Delay(1500);
            requestInfoChoice();
        }

        private void readAndUpdateInfoChoice()
        {
            for (int i=0; i< chkList.Length-1; i++)
            {
                chkList[i] = infoChoice & 1;
                infoChoice = infoChoice >> 1;
            }

            for (int i =0; i< numberOfCheckBox; i++)
            {
                if (chkList[i] == 1) chkBoxArr[i].IsChecked = true;
                else chkBoxArr[i].IsChecked = false;
            }
            
        }

        private void itemChecked(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(chkEuler)) chkList[0] = 1;
            if (sender.Equals(chkGyro)) chkList[1] = 1;
            if (sender.Equals(chkAcc)) chkList[2] = 1;
            if (sender.Equals(chkMag)) chkList[3] = 1;
            if (sender.Equals(chkMotor)) chkList[4] = 1;
            if (sender.Equals(chkThrottle)) chkList[5] = 1;
            if (sender.Equals(chkLocalCoord)) chkList[6] = 1;
            if (sender.Equals(chkDistGround)) chkList[7] = 1;
            if (sender.Equals(chkAttPidOut)) chkList[8] = 1;
            if (sender.Equals(chkNavPidOut)) chkList[9] = 1;
            if (sender.Equals(chkAttCtrlRef)) chkList[10] = 1;
            if (sender.Equals(chkNavCtrlRef)) chkList[11] = 1;
            updateInfoChoice();
        }

        private void itemUnchecked(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(chkEuler)) chkList[0] = 0;
            if (sender.Equals(chkGyro)) chkList[1] = 0;
            if (sender.Equals(chkAcc)) chkList[2] = 0;
            if (sender.Equals(chkMag)) chkList[3] = 0;
            if (sender.Equals(chkMotor)) chkList[4] = 0;
            if (sender.Equals(chkThrottle)) chkList[5] = 0;
            if (sender.Equals(chkLocalCoord)) chkList[6] = 0;
            if (sender.Equals(chkDistGround)) chkList[7] = 0;
            if (sender.Equals(chkAttPidOut)) chkList[8] = 0;
            if (sender.Equals(chkNavPidOut)) chkList[9] = 0;
            if (sender.Equals(chkAttCtrlRef)) chkList[10] = 0;
            if (sender.Equals(chkNavCtrlRef)) chkList[11] = 0;
            updateInfoChoice();
        }

        private void updateInfoChoice()
        {
            string temp = null;
            for(int i= chkList.Length-1; i>=0; i--)
            {
                infoChoice = (infoChoice << 1) | chkList[i];
                if (( (i+1) % 4 == 0) && i != chkList.Length - 1) temp += "_";
                temp = temp + chkList[i].ToString();
            }
            txtBin.Text = temp;
            txtDec.Text = infoChoice.ToString();
        }

        private void btnCheckDefault_Click(object sender, RoutedEventArgs e)
        {
            
            List<int> tempList = new List<int>();
            tempList.Add(0);
            tempList.Add(1);
            tempList.Add(2);
            tempList.Add(3);
            tempList.Add(4);
            tempList.Add(5);
            tempList.Add(6);
            tempList.Add(7);
            for (int i = 0; i< numberOfCheckBox; i++)
            {
                if (tempList.Contains(i)) chkBoxArr[i].IsChecked = true;
                else chkBoxArr[i].IsChecked = false;
            }

        }

        private void btnChekcMonitor_Click(object sender, RoutedEventArgs e)
        {
            List<int> tempList = new List<int>();
            tempList.Add(0);
            tempList.Add(4);
            tempList.Add(5);
            tempList.Add(6);
            tempList.Add(7);
            tempList.Add(10);
            for (int i = 0; i < numberOfCheckBox; i++)
            {
                if (tempList.Contains(i)) chkBoxArr[i].IsChecked = true;
                else chkBoxArr[i].IsChecked = false;
            }
        }

        private void btnJoystick_Click(object sender, RoutedEventArgs e)
        {
            List<int> tempList = new List<int>();
            tempList.Add(0);
            tempList.Add(5);
            tempList.Add(6);
            tempList.Add(7);
            tempList.Add(10);
            for (int i = 0; i < numberOfCheckBox; i++)
            {
                if (tempList.Contains(i)) chkBoxArr[i].IsChecked = true;
                else chkBoxArr[i].IsChecked = false;
            }
        }

        private void pidAttDebug_Click(object sender, RoutedEventArgs e)
        {
            pidAttDebugMode();
        }

        private void pidAttDebugMode()
        {
            List<int> tempList = new List<int>();
            tempList.Add(0);
            tempList.Add(4);
            tempList.Add(5);
            tempList.Add(8);

            for (int i = 0; i < numberOfCheckBox; i++)
            {
                if (tempList.Contains(i)) chkBoxArr[i].IsChecked = true;
                else chkBoxArr[i].IsChecked = false;
            }
        }

        public void autoSetPidAttDebugMode()
        {
            pidAttDebugMode();
            setup();
        }

        public void autoClosePidAttDebugMode()
        {
            itemUnchecked(chkAttPidOut, new RoutedEventArgs());
            setup();
        }
        private void pidNavDebug_Click(object sender, RoutedEventArgs e)
        {
            pidNavDebugMode();
        }

        private void pidNavDebugMode()
        {
            List<int> tempList = new List<int>();
            tempList.Add(0);
            tempList.Add(4);
            tempList.Add(5);
            tempList.Add(6);
            tempList.Add(9);
            tempList.Add(10);
            for (int i = 0; i < numberOfCheckBox; i++)
            {
                if (tempList.Contains(i)) chkBoxArr[i].IsChecked = true;
                else chkBoxArr[i].IsChecked = false;
            }
        }

        public void autoSetPidNavDebugMode()
        {
            pidNavDebugMode();
            setup();
        }

        public void autoClosePidNavDebugMode()
        {
            itemUnchecked(chkNavPidOut, new RoutedEventArgs());
            setup();
        }

    }
}
