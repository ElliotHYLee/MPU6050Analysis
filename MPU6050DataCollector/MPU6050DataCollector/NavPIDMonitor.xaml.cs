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

        static private NavPIDMonitor _instance;
        private MainController _mainCtrl;

        static public NavPIDMonitor Instance()
        {
            if (_instance == null) _instance = new NavPIDMonitor();
            return _instance;
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

        private void btnSync_Click(object sender, RoutedEventArgs e)
        {
            txtKpPitchSet.Text = txtKpPitch.Text;
            txtKdPitchSet.Text = txtKdPitch.Text;
            txtKiPitchSet.Text = txtKiPitch.Text;

            txtKpRollSet.Text = txtKpRoll.Text;
            txtKdRollSet.Text = txtKdRoll.Text;
            txtKiRollSet.Text = txtKiRoll.Text;

            txtKpHeightAscSet.Text = txtKpHeightAsc.Text;
            txtKpHeightDescSet.Text = txtKpHeightDesc.Text;
            txtKdHeightAscSet.Text = txtKdHeightAsc.Text;
            txtKdHeightDescSet.Text = txtKdHeightDesc.Text;
            txtKiHeightSet.Text = txtKiHeight.Text;



        }

        private void btnSetPitch_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.updatePidConstX();
        }

        private void btnSetRoll_Click(object sender, RoutedEventArgs e)
        {
            this._mainCtrl.updatePidConstY();
        }
    }
}
