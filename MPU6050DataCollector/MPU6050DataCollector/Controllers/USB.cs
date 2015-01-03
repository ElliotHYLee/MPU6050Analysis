using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using MPU6050DataCollector;
using MPU6050DataCollector.Model;



namespace SerialMonitorTest03.ControllerFolder
{
    // This class doesn't check convention
    class USB
    {
        private MainWindow _main;
        private AttitudeData _data;
        private SerialPort _serial;

        private string[] _portsList;
        private int _numberOfPorts;
        private bool _collectionMode;

        public USB(MainWindow x, AttitudeData y)
        {
            this._main = x;
            this._data = y;
            this._serial = new SerialPort();
            this._portsList = SerialPort.GetPortNames();
            this._numberOfPorts = this._portsList.Length;
            this._collectionMode = false;
        }

        #region Getter and Setter

            public bool collectionMode
            {
                set { this.collectionMode = value; }
                get { return this._collectionMode; }
            }

            public int numberOfPorts
            {
                set { }
                get 
                {
                    this._portsList = SerialPort.GetPortNames();
                    this._numberOfPorts = this._portsList.Length;
                    return this._numberOfPorts; 
                }
             }

            public String[] portsList 
            { 
                set { }
                get { return this._portsList; }
            }

            public String portName
            {
                set { } 
                get { return this._serial.PortName; }
            }

        #endregion 

        #region connect and disconnect

            public Boolean disconnect()
            {
                bool result = false;
                this._serial.Close();
                result = false;
                this.keepUsbConvention(result);
                return result;
            }
        
            public bool connect(string portName, int baudRate)
            {
                bool result = false;
                this._serial.PortName = portName;
                this._serial.BaudRate = baudRate;
                // connecting empty event to the actual method
                // LHS is empty event for serial object, which original has this empty even handler(method)
                // RHS is the actual methods that I have to implement
                this._serial.DataReceived += serial_DataReceived;
                this._serial.Open();
                result = true;
                this.keepUsbConvention(result);
                return result;
            }

            private void keepUsbConvention(bool status)
            {
                if (status) // connection is true
                {
                    this._main.btnRefresh.IsEnabled = false;
                    this._main.btnConnect.Content = "Disconnect";
                }
                else // no connection
                {
                    this._main.btnRefresh.IsEnabled = true;
                    this._main.btnConnect.Content = "Connect";
                }
                
            }

        #endregion

        #region Data receiving

            private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
            {
                string indata = (string)this._serial.ReadExisting();
                string strToParse = this.trim(indata);
                this.parse(strToParse);
                this.updateMain();
            }

            private string trim(string x)
            {
                Console.WriteLine(x);
                int beginPos = this.findFirstChar(x, '[', true);
                int endPos = this.findFirstChar(x, ']', false);

                string result = x.Substring(beginPos, x.Length - endPos);

                return result;
            }

            private string reverse(string str)
            {
                char[] array = str.ToCharArray();
                Array.Reverse(array);
                return new String(array);
            }

            private int findFirstChar(string str, char x, bool fromFront)
            {
                int result = 0;
                if (fromFront)
                {
                    result = str.IndexOf(x.ToString());
                }
                else
                {
                    string revStr = reverse(str);
                    int position = revStr.IndexOf(x.ToString()) + 1;
                    result = str.Length - position;
                }
                return result;
            }

            private void parse(string indata )
            {
                //Console.WriteLine(indata);
                //Console.WriteLine("==========");
            }

                   
            

            private void updateMain()
            {
            
            }

        #endregion

        #region Data Sending

            public void sendData(String value)
            {
                this._serial.WriteLine(value);
            }

        #endregion


    }
}
