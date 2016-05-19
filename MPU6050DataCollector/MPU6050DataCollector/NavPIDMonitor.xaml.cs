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

        static public NavPIDMonitor Instance()
        {
            if (_instance == null) _instance = new NavPIDMonitor();
            return _instance;
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
    }
}
