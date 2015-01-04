using MPU6050DataCollector.Controllers;
using MPU6050DataCollector.Model;
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

        public MainWindow()
        {
            InitializeComponent();
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

 
    }
}
