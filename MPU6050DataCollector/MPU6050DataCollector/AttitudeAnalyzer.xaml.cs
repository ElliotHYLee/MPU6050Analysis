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

        public MainWindow()
        {
            InitializeComponent();
            this._data = new AttitudeData();
            this._ctrl = new MainController(this, this._data);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
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

 
    }
}
