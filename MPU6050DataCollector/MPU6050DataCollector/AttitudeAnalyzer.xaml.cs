/*============================================================
// Made by: Elliot Hongyun Lee
// Undergrad Research Project
============================================================*/

using MPU6050DataCollector.Controllers;
using MPU6050DataCollector.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MPU6050DataCollector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private MainController _ctrl;
        private AttitudeData _data;
        private TextBox[] _gyro;
        private TextBox[] _acc;
        private TextBox[] _cFilter;

        private ProgressBar[] _cellBar;
        private TextBox[] _cellInfo;
        

        public MainWindow()
        {
            InitializeComponent();

            _cellBar = new ProgressBar[6];
            _cellInfo = new TextBox[6];

            _cellBar[0] = prgLipoC0;
            _cellBar[1] = prgLipoC1;
            _cellBar[2] = prgLipoC2;
            _cellBar[3] = prgLipoC3;
            _cellBar[4] = prgLipoC4;
            _cellBar[5] = prgLipoC5;

            _cellInfo[0] = txtLipoC0;
            _cellInfo[1] = txtLipoC1;
            _cellInfo[2] = txtLipoC2;
            _cellInfo[3] = txtLipoC3;
            _cellInfo[4] = txtLipoC4;
            _cellInfo[5] = txtLipoC5;

            //prgLipoC0.Value = 10;
            //prgLipoC0.Foreground = new SolidColorBrush(Colors.Red);

            Color color = new Color();
            color = Colors.Azure;
            this.Background = new SolidColorBrush(color);


            this._data = new AttitudeData();
            this._ctrl = new MainController(this, this._data);
            this._gyro = new TextBox[10];
            this._acc = new TextBox[10];
            this._cFilter = new TextBox[10];

            this._gyro[0] = this.txtGRawX;
            this._gyro[1] = this.txtGRawY;
            this._gyro[2] = this.txtGRawZ;
            this._gyro[3] = this.txtGNorm;
            this._gyro[4] = this.txtGDirCosX;
            this._gyro[5] = this.txtGDegreeX;
            this._gyro[6] = this.txtGDirCosY;
            this._gyro[7] = this.txtGDegreeY;
            this._gyro[8] = this.txtGDirCosZ;
            this._gyro[9] = this.txtGDegreeZ;
            this._acc[0] = this.txtARawX;
            this._acc[1] = this.txtARawY;
            this._acc[2] = this.txtARawZ;
            this._acc[3] = this.txtANorm;
            this._acc[4] = this.txtADirCosX;
            this._acc[5] = this.txtADegreeX;
            this._acc[6] = this.txtADirCosY;
            this._acc[7] = this.txtADegreeY;
            this._acc[8] = this.txtADirCosZ;
            this._acc[9] = this.txtADegreeZ;
            this._cFilter[0] = this.txtCRawX;
            this._cFilter[1] = this.txtCRawY;
            this._cFilter[2] = this.txtCRawZ;
            this._cFilter[3] = this.txtCNorm;
            this._cFilter[4] = this.txtCDirCosX;
            this._cFilter[5] = this.txtCDegreeX;
            this._cFilter[6] = this.txtCDirCosY;
            this._cFilter[7] = this.txtCDegreeY;
            this._cFilter[8] = this.txtCDirCosZ;
            this._cFilter[9] = this.txtCDegreeZ;
        }


        public TextBox[] CellInfo
        {
            get { return _cellInfo; }
        }

        public ProgressBar[] CellBar
        {
            get { return _cellBar; }
        }


        public TextBox[] gyro
        {
            set{;}
            get{ return this._gyro;}
        }

        public TextBox[] acc
        {
            set { ;}
            get { return this._acc; }
        }

        public TextBox[] cFilter
        {
            set { ;}
            get { return this._cFilter; }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.lblBoard.Content = "Saving as Excel takes a whiel. Just wait a little...";
            this._ctrl.saveToExcel();
        }

        private void btnConnect_Clicked(object sender, RoutedEventArgs e)
        {
            this._ctrl.connect();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.refreshComports();
        }

        private void btnCollect_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.startCollecting();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._ctrl.Loaded();
            //this._main.moterSlide1 = new Slider();
            //this._main.motorSlide2 = new Slider();
            //this._main.motorSlide3 = new Slider();
            //this._main.motorSlide4 = new Slider();

        }

        private void txtCDegreeX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this._ctrl.updateAttitudeX(((TextBox)sender).Text);

                
                this._ctrl.updateAttitudeY(this.txtCDegreeY.Text);
            }
            catch (Exception ex)
            {
                ex.GetType();
            }
            
        }

        private void btnDecreaseAll_Click(object sender, RoutedEventArgs e)
        {
            //this._ctrl.decrease(1, this.txtMotor1.Text, int.Parse(this.txtDecraseCalInterval.Text));
            //this._ctrl.decrease(2, this.txtMotor2.Text, int.Parse(this.txtDecraseCalInterval.Text));
            //this._ctrl.decrease(3, this.txtMotor3.Text, int.Parse(this.txtDecraseCalInterval.Text));
            //this._ctrl.decrease(4, this.txtMotor4.Text, int.Parse(this.txtDecraseCalInterval.Text));
            //this._ctrl.decrease(5, this.txtMotor5.Text, int.Parse(this.txtDecraseCalInterval.Text));
            //this._ctrl.decrease(6, this.txtMotor6.Text, int.Parse(this.txtDecraseCalInterval.Text));
            this._ctrl.decreaseAll(int.Parse(this.txtDecraseCalInterval.Text));

        }

        private void btnStartPWM_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.prepareMode();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.idleMode();
        }

        private void btnIncreaseAll_Click(object sender, RoutedEventArgs e)
        {
            //this._ctrl.increase(1, this.txtMotor1.Text, int.Parse(this.txtIncraseCalInterval.Text));
            //this._ctrl.increase(2, this.txtMotor2.Text, int.Parse(this.txtIncraseCalInterval.Text));
            //this._ctrl.increase(3, this.txtMotor3.Text, int.Parse(this.txtIncraseCalInterval.Text));
            //this._ctrl.increase(4, this.txtMotor4.Text, int.Parse(this.txtIncraseCalInterval.Text));
            //this._ctrl.increase(5, this.txtMotor5.Text, int.Parse(this.txtIncraseCalInterval.Text));
            //this._ctrl.increase(6, this.txtMotor6.Text, int.Parse(this.txtIncraseCalInterval.Text));

            this._ctrl.increaseAll(int.Parse(this.txtIncraseCalInterval.Text));

        }

        private void btnIncreaseMotor1_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(1, this.txtMotor1.Text, 1);
        }

        private void btnIncreaseMotor2_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(2, this.txtMotor2.Text, 1);
        }

        private void btnIncreaseMotor3_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(3, this.txtMotor3.Text, 1);
        }


        private void btnIncreaseMotor4_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(4, this.txtMotor4.Text, 1);
        }
        private void btnIncreaseMotor5_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(5, this.txtMotor5.Text, 1);
        }

        private void btnIncreaseMotor6_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(6, this.txtMotor6.Text, 1);
        }



        private void btnDecreaseMotor1_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(1, this.txtMotor1.Text, 1);
        }

        private void btnDecreaseMotor2_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(2, this.txtMotor2.Text, 1);
        }

        private void btnDecreaseMotor3_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(3, this.txtMotor3.Text, 1);
        }

        private void btnDecreaseMotor4_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(4, this.txtMotor4.Text, 1);
        }
        private void btnDecreaseMotor5_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(5, this.txtMotor5.Text, 1);
        }
        private void btnDecreaseMotor6_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(6, this.txtMotor6.Text, 1);
        }

        private void btnGetPidConst_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.requestAttPidConst();
        }

        //private void btnSetPidConst_Click(object sender, RoutedEventArgs e)
        //{
        //    this._ctrl.updatePidConst();
        //}

        private void moterSlide1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }



        private void btnDecreaseMotor1By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(1, this.txtMotor1.Text, int.Parse(this.txtDecraseCalInterval.Text));
        }

        private void btnDecreaseMotor2By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(2, this.txtMotor2.Text, int.Parse(this.txtDecraseCalInterval.Text));
        }

        private void btnDecreaseMotor3By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(3, this.txtMotor3.Text, int.Parse(this.txtDecraseCalInterval.Text));
        }

        private void btnDecreaseMotor4By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(4, this.txtMotor4.Text, int.Parse(this.txtDecraseCalInterval.Text));
        }



        private void btnDecreaseMotor5By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(5, this.txtMotor5.Text, int.Parse(this.txtDecraseCalInterval.Text));
        }

      

        private void btnDecreaseMotor6By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.decrease(6, this.txtMotor6.Text, int.Parse(this.txtDecraseCalInterval.Text));
        }




        private void btnIncreaseMotor1By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(1, this.txtMotor1.Text, int.Parse(this.txtIncraseCalInterval.Text));
        }
        private void btnIncreaseMotor2By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(2, this.txtMotor2.Text, int.Parse(this.txtIncraseCalInterval.Text));
        }
        private void btnIncreaseMotor3By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(3, this.txtMotor3.Text, int.Parse(this.txtIncraseCalInterval.Text));
        }
        private void btnIncreaseMotor4By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(4, this.txtMotor4.Text, int.Parse(this.txtIncraseCalInterval.Text));
        }

        private void btnIncreaseMotor5By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(5, this.txtMotor5.Text, int.Parse(this.txtIncraseCalInterval.Text));
        }
        private void btnIncreaseMotor6By_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.increase(6, this.txtMotor6.Text, int.Parse(this.txtIncraseCalInterval.Text));
        }

        private void btnOpenPID_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.openAttPIDMonitor();
        }

        

        
        

        private void txtMotor6_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtMotor4_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void motorSlide4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void btnThrottleUp100_Click(object sender, RoutedEventArgs e)
        {
            int newThrottle = int.Parse(this.txtMainThrottle.Text) + 100;
            this._ctrl.updateThrottle(newThrottle);
        }

        private void btnThrottleUp50_Click(object sender, RoutedEventArgs e)
        {
            int newThrottle = int.Parse(this.txtMainThrottle.Text) + 50;
            this._ctrl.updateThrottle(newThrottle);
        }

        private void btnThrottleDown50_Click(object sender, RoutedEventArgs e)
        {
            int newThrottle = int.Parse(this.txtMainThrottle.Text) - 50;
            this._ctrl.updateThrottle(newThrottle);
        }

        private void btnThrottleDown100_Click(object sender, RoutedEventArgs e)
        {
            int newThrottle = int.Parse(this.txtMainThrottle.Text) - 100;
            this._ctrl.updateThrottle(newThrottle);
        }

        private void sldrMainThrottle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void btnSetThrottle_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.updateThrottle(int.Parse(this.txtSetThrottle.Text));
        }


        private void txtCRawX_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this._ctrl.updateAttitudeX(((TextBox)sender).Text);
                this._ctrl.updateAttitudeY(this.txtCRawY.Text);
                this._ctrl.updateAttitudeZ(this.txtCRawZ.Text);
              
            }
            catch (Exception ex)
            {
                ex.GetType();
            }
        }


        private void btnJoyStick_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.openJoyStick();
        }

        private void txtCRawY_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnPreTakeOff_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.preTakeOffMode();
        }

        private void btnTakeOff_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.takeOffMode();
        }

        private void btnOPenNavPID_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.openNavPIDMonitor();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.testSend();
        }

        private void btnInfoChoice_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.openInfoChoiceMonitor();
        }

        private void btnSD_Click(object sender, RoutedEventArgs e)
        {
            this._ctrl.SDRecording();
        }







        //private void txtCDegreeX_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    this._ctrl.updateAttitude(((TextBox) sender).Text);
        //}


    }
}
