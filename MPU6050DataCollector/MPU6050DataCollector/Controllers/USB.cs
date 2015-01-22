using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using MPU6050DataCollector;
using MPU6050DataCollector.Model;
using System.Windows;
using System.Windows.Media;
using MPU6050DataCollector.Controllers;

namespace SerialMonitorTest03.ControllerFolder
{
    // This class doesn't check convention
    class USB
    {
        //private bool regulator = true;
        private MainWindow _main;
        private AttitudeData _data;
        private SerialPort _serial;
        private MainController _mainCtrl;
        private string _testInstream;


        private string[] _portsList;
        private int _numberOfPorts;
        private bool _collectionMode;
        private int _numberOfDataGathered;


        public USB(MainWindow x, AttitudeData y, MainController z)
        {
            this._mainCtrl = z;
            this._main = x;
            this._data = y;
            this._serial = new SerialPort();
            this._portsList = SerialPort.GetPortNames();
            this._numberOfPorts = this._portsList.Length;
            this._collectionMode = false;
            this._numberOfDataGathered = 0;
        }

        #region Getter and Setter

            public bool collectionMode
            {
                set { this._collectionMode = value; }
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
                try
                {
                    this._serial.Open();
                }
                catch (Exception ee)
                {
                    MessageBox.Show("wait for loading ends");
                    return false;
                }
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
                
                string inStream = (string)this._serial.ReadExisting();
                //if (inStream.Contains("p"))
                //{
                //    Console.WriteLine(inStream);
                //}
                //Console.WriteLine(inStream);
                this._testInstream = inStream;
                try
                {
                    this.parse(inStream);
                    //if (regulator)// parse 50 % of incoming data
                    //{
                    //    //Console.WriteLine(inStream);
                          //this.parse(inStream);
                    //}
                    //regulator = !regulator; 

                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }

            private void parse(string x)
            {

                string raw = Tokenizer.Tokenizer.trim(x);
                if (raw.Equals("abort"))
                {
                    return;
                }

                List<string> listTokens = Tokenizer.Tokenizer.getTokens(raw);
                ISet<string> setInfoTypes = new HashSet<string>();
                
                string infoType = "";
                string infoDir = "";

                string[] gyro = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                string[] acc = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                string[] cFilter = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                string[] motor = { "0", "0", "0", "0"};
                string[] pid = { "e", "e", "e" };

                #region parsing tokens


                string pidOut = "";
                string pidProportional="";
                string pidDerivative="";
                for (int i = 0; i < listTokens.Count; i++)
                {
                    infoType = listTokens[i].Substring(0, 1);

                    // report types of information incoming
                    if (!setInfoTypes.Contains(infoType))
                    {
                        setInfoTypes.Add(infoType);
                    }

                    // check what type of information it is
                    if (infoType.Equals("g"))
                    {
                        #region parse gyro
                        // get direction of info (x,y,z)
                        infoDir = listTokens[i].Substring(1, 1);
                        switch (infoDir)
                        {
                            case "x":
                                gyro[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "y":
                                gyro[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "z":
                                gyro[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                        }
                        #endregion
                    }
                    else if (infoType.Equals("a"))
                    {
                        #region parse acc
                        // get direction of info (x,y,z)

                        infoDir = listTokens[i].Substring(1, 1);
                        switch (infoDir)
                        {
                            case "x":
                                acc[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "y":
                                acc[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "z":
                                acc[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                        }
                        #endregion
                    }
                    else if (infoType.Equals("c"))
                    {
                        #region parse cFilter
                        // get direction of info (x,y,z)
                        infoDir = listTokens[i].Substring(1, 1);
                        switch (infoDir)
                        {
                            case "x":
                                cFilter[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "y":
                                cFilter[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "z":
                                cFilter[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                        }
                        #endregion
                    }
                    else if (infoType.Equals("m"))
                    {
                        #region parse motor
                        // get direction of info 1,2,3,4 motor
                        infoDir = listTokens[i].Substring(1, 1);
                        switch (infoDir)
                        {
                            case "1":
                                motor[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "2":
                                motor[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "3":
                                motor[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "4":
                                motor[3] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                        }
                        #endregion
                    }
                    else if (infoType.Equals("k"))
                    {
                        
                        if (listTokens[i].Substring(1, 1).Equals("0"))
                        {
                            pidOut = listTokens[i].Substring(2, listTokens[i].Length - 2);
                        }
                        else if (listTokens[i].Substring(1, 1).Equals("1"))
                        {
                            pidProportional = listTokens[i].Substring(2, listTokens[i].Length - 2);
                        }
                        else if (listTokens[i].Substring(1, 1).Equals("2"))
                        {

                        }
                        else if (listTokens[i].Substring(1, 1).Equals("3"))
                        {
                            pidDerivative = listTokens[i].Substring(2, listTokens[i].Length - 2);
                        }
                    }
                    else if (infoType.Equals("p"))
                    {
                        // get direction of info 1,2,3,4 motor
                        infoDir = listTokens[i].Substring(1, 1);
                        switch (infoDir)
                        {
                            case "p":
                                pid[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "i":
                                pid[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "d":
                                pid[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                                break;
                            case "o":
                                Console.WriteLine("PID On/Off");
                                if (listTokens[i].Substring(2, listTokens[i].Length - 2).Equals("1"))
                                {
                                    this._mainCtrl.pidOnOff = true;
                                }
                                else
                                {
                                    this._mainCtrl.pidOnOff = false;
                                }
                                break;
                           
                            
                        }
                    }

                }
                #endregion

                this.updateMain(gyro, acc, cFilter, motor); 
                

                this._main.Dispatcher.Invoke(() =>
                {
                    this._main.txtPidOutput.Text = pidOut;
                    this._main.txtPidProportional.Text = pidProportional;
                    this._main.txtPidDerivative.Text = pidDerivative;


                    this._main.listInfoType.Items.Clear();
                    List<string> temp = setInfoTypes.ToList<string>();
                    for(int i = 0; i < setInfoTypes.Count; i++)
                    {
                        
                        temp.Sort();
                        if (temp[i].Equals("a")){
                            this._main.listInfoType.Items.Add("a: Accelerometer");
                        }else if(temp[i].Equals("g")){
                            this._main.listInfoType.Items.Add("g: Gyroscope");
                        }else if(temp[i].Equals("c")){
                            this._main.listInfoType.Items.Add("c: Complementary Filter");
                        }else if (temp[i].Equals("m")){
                            this._main.listInfoType.Items.Add("m: Motor PWM");
                        }
                        else if (temp[i].Equals("p")){
                            this._main.listInfoType.Items.Add("p: PID");

                            if (!pid[0].Equals("e"))
                            {
                                this._main.txtKp.Text = pid[0];
                                this._main.txtKi.Text = pid[1];
                                this._main.txtKd.Text = pid[2];
                            }
                            
                            if (this._mainCtrl.pidOnOff)
                            {
                                SolidColorBrush dd = new SolidColorBrush();
                                dd.Color = Colors.Green;
                                this._main.lblPidIndicator.Content = "PID ON";
                                this._main.lblPidIndicator.Background = dd;
                            }
                            else
                            {
                                SolidColorBrush dd = new SolidColorBrush();
                                dd.Color = Colors.Red;
                                this._main.lblPidIndicator.Content = "PID OFF";
                                this._main.lblPidIndicator.Background = dd;
                            }
                        }
                        
                        
                        
                    }
                    temp.Clear();
                    
                });
                setInfoTypes.Clear();
            }

            private void updateMain(string[] gyro, string[] acc, string[] cFilter, string[] motor )
            {

                #region for loop calculating attitudes
                
                double norm, xRaw, yRaw, zRaw;
                // this for loop calculates variables.
                for (int i = 0; i < 3; i++)
                {

                    try 
                    {
                        if (i == 0) // gyro
                        {
                            xRaw = Double.Parse(gyro[0]);
                            yRaw = Double.Parse(gyro[1]);
                            zRaw = Double.Parse(gyro[2]);
                            norm = Math.Sqrt(xRaw * xRaw + yRaw * yRaw + zRaw * zRaw);
                            gyro[3] = norm.ToString();
                            gyro[4] = (xRaw / norm).ToString();
                            gyro[5] = (Math.Acos(xRaw / norm) * 180 / Math.PI).ToString();
                            gyro[6] = (yRaw / norm).ToString();
                            gyro[7] = (Math.Acos(yRaw / norm) * 180 / Math.PI).ToString();
                            gyro[8] = (zRaw / norm).ToString();
                            gyro[9] = (Math.Acos(zRaw / norm) * 180 / Math.PI).ToString();

                        }
                        else if (i == 1) //acc
                        {
                            xRaw = Double.Parse(acc[0]);
                            yRaw = Double.Parse(acc[1]);
                            zRaw = Double.Parse(acc[2]);
                            norm = Math.Sqrt(xRaw * xRaw + yRaw * yRaw + zRaw * zRaw);
                            if (norm != 0)
                            {
                                acc[3] = norm.ToString();
                                acc[4] = (xRaw / norm).ToString();
                                acc[5] = (Math.Acos(xRaw / norm) * 180 / Math.PI).ToString();
                                acc[6] = (yRaw / norm).ToString();
                                acc[7] = (Math.Acos(yRaw / norm) * 180 / Math.PI).ToString();
                                acc[8] = (zRaw / norm).ToString();
                                acc[9] = (Math.Acos(zRaw / norm) * 180 / Math.PI).ToString();
                            }
                        }
                        else if (i==2)
                        {
                            xRaw = Double.Parse(cFilter[0]);
                            yRaw = Double.Parse(cFilter[1]);
                            zRaw = Double.Parse(cFilter[2]);
                            norm = Math.Sqrt(xRaw * xRaw + yRaw * yRaw + zRaw * zRaw);
                            if (norm != 0)
                            {
                                cFilter[3] = norm.ToString();
                                cFilter[4] = (xRaw / norm).ToString();
                                cFilter[5] = (Math.Acos(xRaw / norm) * 180 / Math.PI).ToString();
                                cFilter[6] = (yRaw / norm).ToString();
                                cFilter[7] = (Math.Acos(yRaw / norm) * 180 / Math.PI).ToString();
                                cFilter[8] = (zRaw / norm).ToString();
                                cFilter[9] = (Math.Acos(zRaw / norm) * 180 / Math.PI).ToString();
                            }

                        }
                    }catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("acc[0]=" + acc[0]);
                        Console.WriteLine("acc[1]=" + acc[1]);
                        Console.WriteLine("acc[2]=" + acc[2]);
                        Console.WriteLine("gyro[0]=" + gyro[0]);
                        Console.WriteLine("gyro[0]=" + gyro[1]);
                        Console.WriteLine("gyro[0]=" + gyro[2]);
                        Console.WriteLine("cFilter[0]=" + cFilter[0]);
                        Console.WriteLine("cFilter[0]=" + cFilter[1]);
                        Console.WriteLine("cFilter[0]=" + cFilter[2]);

                    }

                }
                #endregion

                #region for collection mode

                if (this._collectionMode)
                {
                    Console.WriteLine("gathering...");
                    for (int i = 0; i < 10; i++)
                    {
                        this._data.gyro[i].Add(gyro[i]);
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        this._data.acc[i].Add(acc[i]);
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        this._data.cFilter[i].Add(cFilter[i]);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        this._data.motor[i].Add(motor[i]);
                    }
                    this._numberOfDataGathered++;
                }
                else
                {
                    this._numberOfDataGathered = 0;
                }

                #endregion

                #region update main window

                this._main.Dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        this._main.gyro[i].Text = gyro[i];
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        this._main.acc[i].Text = acc[i];
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        this._main.cFilter[i].Text = cFilter[i];
                        if (this._main.cFilter[i].Text.Equals("0"))
                        {
                            //Console.WriteLine("/" + this._testInstream+ "/");
                        }
                    }

                    this._main.txtMotor1.Text = motor[0];
                    this._main.txtMotor2.Text = motor[1];
                    this._main.txtMotor3.Text = motor[2];
                    this._main.txtMotor4.Text = motor[3];
                    this._mainCtrl.updateSlider();
                    if (this._numberOfDataGathered > 0)
                    {
                        this._main.txtNumberOfData.Text = this._numberOfDataGathered.ToString() + " data gathered.";
                    }
                    
                });



                #endregion


            }

        #endregion

        #region Data Sending

            public void sendData(String value)
            {
                try
                {
                    this._serial.WriteLine(value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Check usb connection");
                }
                
            }

        #endregion


    }
}
