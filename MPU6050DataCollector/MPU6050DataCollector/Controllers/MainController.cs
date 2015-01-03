using MPU6050DataCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;
using System.Windows;
using SerialMonitorTest03.ControllerFolder;

namespace MPU6050DataCollector.Controllers
{
    class MainController
    {
        private MainWindow _main;
        private AttitudeData _data;
        private USB _usb;
        private bool _usbConnected;
                
        public MainController(MainWindow x, AttitudeData y)
        {
            this._main = x;
            this._data = y;
            this._usb = new USB(x, y);
            this._usbConnected = false;

            this.refreshComports();
        }

        #region connection and disconnection

            private void keepConnectionButtonConvention()
            {
                if (this._usb.numberOfPorts > 0)
                {
                    this._main.btnConnect.IsEnabled = true;
                }
                else
                {
                    this._main.btnConnect.IsEnabled = false;
                }
            }
        
            public void connect()
            {
                if (this._usbConnected)
                {
                    this._usbConnected = this._usb.disconnect();
                    this.refreshComports();
                }
                else // not yet connected. So connect usb here
                {
                    string portName = this._main.comboPorts.SelectedItem.ToString();
                    int baudRate = int.Parse(this._main.comboBaudRate.SelectedItem.ToString());
                    this._usbConnected = this._usb.connect(portName, baudRate);
                    Console.WriteLine(this._usb.portName + " online.");
                }
            
            }
        
            public void refreshComports()
            {
                this._main.comboPorts.Items.Clear();
                this._main.comboBaudRate.Items.Clear();
                Console.WriteLine("Number of Ports Available: " + this._usb.numberOfPorts);
                String[] ports = _usb.portsList;
                for (int i = 0; i < _usb.numberOfPorts; i++)
                {
                    this._main.comboPorts.Items.Insert(i, ports[i]);
                }

                this._main.comboBaudRate.Items.Insert(0, 9600);
                this._main.comboBaudRate.Items.Insert(0, 115200);
            

                if (this._usb.numberOfPorts > 0)
                {
                
                    this._main.comboPorts.SelectedIndex = 0;
                    this._main.comboBaudRate.SelectedIndex = 0;
                }

                this.keepConnectionButtonConvention();
            }

        #endregion

        public void saveToExcel()
        {
            Application excel = new Application();
            if (excel == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }
        }



    }
}
