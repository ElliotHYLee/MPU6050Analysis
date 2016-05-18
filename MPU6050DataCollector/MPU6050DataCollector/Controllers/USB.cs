/*============================================================
// Made by: Elliot Hongyun Lee
// Undergrad Research Project
============================================================*/

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
using System.Diagnostics;

namespace SerialMonitorTest03.ControllerFolder
{


    // This class doesn't check convention
    class USB
    {
        private string instreamGlobalVar;
        //private bool regulator = true;
        private MainWindow _main;
        private AttitudeData _data;
        private SerialPort _serial;
        private MainController _mainCtrl;
        private string _testInstream;

        private double _parsingRate;
        private double _parsingCounter;

        private bool[] pidAxisOnOff = { false, false, false, false, false, false };
        private string[] _portsList;
        private int _numberOfPorts;
        private bool _collectionMode;
        private int _numberOfDataGathered;
        private string[] pid = { "e", "e", "e", "e", "e", "e", "e", "e", "e" };

        string[] gyro = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        string[] acc = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        string[] cFilter = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        string[] motor = { "0", "0", "0", "0", "0", "0" };
        string[] mag = { "0", "0", "0" };
        string distanceToGround = "0";
        string[] localCoord = { "0", "0", "0" };
        string[] ctrlReference = { "0", "0", "0" };

        Stopwatch sw;

        private string[] throttle = new string[3];
        private bool updatePidConst = false;
        private bool updatePidOnOffStatus = false;

        public string GetControlRefYaw
        {
            get { return ctrlReference[2]; }
        }
        

        public USB(MainWindow x, AttitudeData y, MainController z)
        {
            sw = new Stopwatch();
            this._mainCtrl = z;
            this._main = x;
            this._data = y;
            this._serial = new SerialPort();
            this._portsList = SerialPort.GetPortNames();
            this._numberOfPorts = this._portsList.Length;
            this._collectionMode = false;
            this._numberOfDataGathered = 0;
            this._parsingRate = 50; //percentage
            this._parsingCounter = 0;
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


        #region event handling when usb receives data
        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
                       


            string inStream = (string)this._serial.ReadExisting();
            this._serial.DiscardInBuffer();
            this._serial.DiscardOutBuffer();
            Console.WriteLine("---------------------");
            Console.WriteLine(inStream);
            Console.WriteLine("---------------------");

            //if (inStream.Contains("p"))
            //{
            //    Console.WriteLine(inStream);
            //}

            this._testInstream = inStream;
            if (inStream.Equals(""))
            {
                return;
            }
            instreamGlobalVar = inStream;
            _parsingRate = 50;
            try
            {
                double targetCount = 100 - _parsingRate;
                double mult = 100 / targetCount;

                if (((int) this._parsingCounter) % ((int) mult) != 0)
                {

                    this.parse(inStream);
                }

                this._parsingCounter++;
                if (this._parsingCounter > 100)
                {
                    this._parsingCounter = 0;
                }
                //if (regulator)// parse 50 % of incoming data
                //{
                //    //Console.WriteLine(inStream);
                        //this.parse(inStream);
                //}
                //regulator = !regulator; 

            }
            catch (Exception err)
            {
                // Console.WriteLine(err);
            }
        }
        #endregion

        private void parse(string x)
        {
            #region preparation for parsing
            string raw = Tokenizer.Tokenizer.trim(x);
            if (raw.Equals("abort"))
            {
                return;
            }

            List<string> listTokens = Tokenizer.Tokenizer.getTokens(raw);
            ISet<string> setInfoTypes = new HashSet<string>();

            string infoType = "";
            string infoDir = "";

            //string[] gyro = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            //string[] acc = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            //string[] cFilter = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            //string[] motor = { "0", "0", "0", "0", "0", "0" };
            //string[] mag = { "0", "0", "0" };
            //string distanceToGround = "0";
            //string[] localCoord = { "0", "0", "0" };
            //string[] ctrlReference = { "0", "0", "0" };
        #endregion

        #region parsing tokens


        string[] pidPro = { "", "", "" };
            string[] pidDer = { "", "", "" };
            string[] pidInt = { "", "", "" };
            string[] pidOutput = { "", "", "" };

            for (int i = 0; i < listTokens.Count; i++)
            {
                infoType = listTokens[i].Substring(0, 1);
    //              Console.WriteLine("type: " + infoType);

                // report types of information incoming
                if (!setInfoTypes.Contains(infoType))
                {
                    setInfoTypes.Add(infoType);
                }

            // check what type of information it is
            if (infoType.Equals('r'))
            { }
                        

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
                if (infoType.Equals("a"))
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
                if (infoType.Equals("c"))
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
                if (infoType.Equals("m"))
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
                        case "5":
                            motor[4] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "6":
                            motor[5] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                    }
                    #endregion
                }
                if (infoType.Equals("q"))
                {
                    #region parse magnetometer
                    // get direction of info 1,2,3 magnetometer
                    infoDir = listTokens[i].Substring(1, 1);
                    switch (infoDir)
                    {
                        case "x":
                            mag[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            //MessageBox.Show("asdf");
                            break;
                        case "y":
                            mag[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "z":
                            mag[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;

                    }
                    #endregion
                }
                if (infoType.Equals("t"))
                {
                    //Console.WriteLine("+====================");
                    #region parse throttle
                    infoDir = listTokens[i].Substring(1, 1);
                    if (infoDir.Equals("0"))
                    {
                        throttle[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                        //Console.WriteLine(throttle[0]);
                    }
                    if (infoDir.Equals("1"))
                    {
                        throttle[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    if (infoDir.Equals("2"))
                    {
                        throttle[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    #endregion
                }
                if (infoType.Equals("k")) // pid calculation
                {
                    //[k0value] : xProportional calc result
                    //[k1value] : xDerivative calc result
                    //[k2value] : xIntegral calc result
                    //[k3value] : xOutput calc result
                    //[k4value] : yProportioanl calc reuslt
                    //....
                    #region parse PIDcalcuations
                    if (listTokens[i].Substring(1, 1).Equals("0"))
                    {
                        pidPro[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("1"))
                    {
                        pidDer[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("2"))
                    {
                        pidInt[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("3"))
                    {
                        pidOutput[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("4"))
                    {
                        pidPro[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("5"))
                    {
                        pidDer[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("6"))
                    {
                        pidInt[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("7"))
                    {
                        pidOutput[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("8"))
                    {
                        pidPro[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("9"))
                    {
                        pidDer[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("A"))
                    {
                        pidInt[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    else if (listTokens[i].Substring(1, 1).Equals("B"))
                    {
                        pidOutput[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    #endregion

                }
                if (infoType.Equals("p"))  // pid constants(gains)
                {
                    this.updatePidConst = true;
                    #region parse pid constants
                    // get pid constatns
                    infoDir = listTokens[i].Substring(1, 1);
                    switch (infoDir)
                    {
                        case "0":
                            pid[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "1":
                            pid[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "2":
                            pid[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "3":
                            pid[3] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "4":
                            pid[4] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "5":
                            pid[5] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "6":
                            pid[6] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "7":
                            pid[7] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "8":
                            pid[8] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                    #endregion
                    }
                }
                if (infoType.Equals("o"))
                {
                    #region pid on off status
                    this.updatePidOnOffStatus = true;
                    // get pid on/off status
                    infoDir = listTokens[i].Substring(1, 1);
                    string onOff;
                    Console.WriteLine(listTokens[i].Substring(1, listTokens[i].Length - 2));
                    switch (infoDir)
                    {
                        case "0":
                            onOff = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            if (onOff.Equals("0"))
                            {
                                this.pidAxisOnOff[0] = false;
                                Console.WriteLine("PC: pid x is off");
                            }
                            if (onOff.Equals("1"))
                            {
                                this.pidAxisOnOff[0] = true;
                                Console.WriteLine("PC: pid x is on");
                            }
                            break;
                        case "1":
                            onOff = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            if (onOff.Equals("0"))
                            {
                                this.pidAxisOnOff[1] = false;
                                Console.WriteLine("PC: pid y is off");
                            }
                            if (onOff.Equals("1"))
                            {
                                this.pidAxisOnOff[1] = true;
                                Console.WriteLine("PC: pid y is on");
                            }
                            break;
                        case "2":
                            onOff = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            if (onOff.Equals("0"))
                            {
                                this.pidAxisOnOff[2] = false;
                                Console.WriteLine("PC: pid z is off");
                            }
                            if (onOff.Equals("1"))
                            {
                                this.pidAxisOnOff[2] = true;
                                Console.WriteLine("PC: pid z is on");
                            }
                            break;
                    }
                    #endregion
                }
                if (infoType.Equals("d")) // distance 2 ground
                {
                    #region distance info
                    if (listTokens[i].Substring(1, 1).Equals("g"))
                    {
                        distanceToGround = listTokens[i].Substring(2, listTokens[i].Length - 2);
                    }
                    #endregion
                }
                if (infoType.Equals("l"))
                {
                    #region parse localCoord
                    // get direction of info (x,y,z)
                    infoDir = listTokens[i].Substring(1, 1);
                    switch (infoDir)
                    {
                        case "x":
                            localCoord[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "y":
                            localCoord[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                        case "z":
                            localCoord[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                            break;
                    }
                    #endregion
                }

            if (infoType.Equals("s"))
            {
                #region parse ctrl reference
                // get direction of info (x,y,z)
                infoDir = listTokens[i].Substring(1, 1);
                switch (infoDir)
                {
                    case "x":
                        ctrlReference[0] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                        break;
                    case "y":
                        ctrlReference[1] = listTokens[i].Substring(2, listTokens[i].Length - 2);
                        break;
                    case "z":
                        ctrlReference[2] = listTokens[i].Substring(2, listTokens[i].Length - 2);

                        break;
                }
                #endregion
            }

        }
            #endregion

            #region update information
            this.updateMain(gyro, acc, cFilter, motor, mag, distanceToGround, localCoord, ctrlReference);
            #endregion

            #region update monitors for information type

            #region updating pid calculation output
            // updating pid monitor for calculated output

            if (this._mainCtrl._pidMonitorIsOpen)
            {
                this._mainCtrl._pidMonitor.Dispatcher.Invoke(() =>
                    {
                        //update pid monitor
                        this._mainCtrl._pidMonitor.txtXPro.Text = pidPro[0];
                        this._mainCtrl._pidMonitor.txtXDer.Text = pidDer[0];
                        this._mainCtrl._pidMonitor.txtXInt.Text = pidInt[0];
                        this._mainCtrl._pidMonitor.txtXOutput.Text = pidOutput[0];

                        this._mainCtrl._pidMonitor.txtYPro.Text = pidPro[1];
                        this._mainCtrl._pidMonitor.txtYDer.Text = pidDer[1];
                        this._mainCtrl._pidMonitor.txtYInt.Text = pidInt[1];
                        this._mainCtrl._pidMonitor.txtYOutput.Text = pidOutput[1];

                        this._mainCtrl._pidMonitor.txtZPro.Text = pidPro[2];
                        this._mainCtrl._pidMonitor.txtZDer.Text = pidDer[2];
                        this._mainCtrl._pidMonitor.txtZInt.Text = pidInt[2];
                        this._mainCtrl._pidMonitor.txtZOutput.Text = pidOutput[2];
                         
                    });
            }
            #endregion

            #region pidConst_on/off_pidMonitor

            // updating pid monitor for pid constants and on/off status in pidMonitor
            if (this._mainCtrl._pidMonitorIsOpen)
            {
                if (this.updatePidConst)
                {
                    this._mainCtrl._pidMonitor.Dispatcher.Invoke(() =>
                    {
                        //update pid constants
                        #region update pid constants
                        this._mainCtrl._pidMonitor.txtXKp.Text = pid[0];
                        this._mainCtrl._pidMonitor.txtXKi.Text = pid[1];
                        this._mainCtrl._pidMonitor.txtXKd.Text = pid[2];
                        this._mainCtrl._pidMonitor.txtYKp.Text = pid[3];
                        this._mainCtrl._pidMonitor.txtYKi.Text = pid[4];
                        this._mainCtrl._pidMonitor.txtYKd.Text = pid[5];
                        this._mainCtrl._pidMonitor.txtZKp.Text = pid[6];
                        this._mainCtrl._pidMonitor.txtZKi.Text = pid[7];
                        this._mainCtrl._pidMonitor.txtZKd.Text = pid[8];
                        #endregion
                    });
                    this.updatePidConst = false;
                }

                Console.WriteLine("PC: this.updatePidOnOffStatus: " + this.updatePidOnOffStatus);
                if (this.updatePidOnOffStatus)
                {
                    this._mainCtrl._pidMonitor.Dispatcher.Invoke(() =>
                    {
                        #region update pid on off
                        //update pid on/off

                        // x axis
                        Console.WriteLine("PC: this._mainCtrl.pidOnOffX: " + this._mainCtrl.pidOnOffX);
                        if (this.pidAxisOnOff[0])
                        {
                            SolidColorBrush dd = new SolidColorBrush();
                            dd.Color = Colors.Green;
                            this._mainCtrl._pidMonitor.btnXonoff.Background = dd;
                            Console.WriteLine("PC: pid x is on so color changed to green");
                            this._mainCtrl._pidMonitor.btnXonoff.Content = "PID On";
                            this._mainCtrl._pidMonitor.pidXStatus = true;
                        }
                        else
                        {
                            SolidColorBrush dd = new SolidColorBrush();
                            dd.Color = Colors.Red;
                            this._mainCtrl._pidMonitor.btnXonoff.Background = dd;
                            this._mainCtrl._pidMonitor.btnXonoff.Content = "PID Off";
                            this._mainCtrl._pidMonitor.pidXStatus = false;
                        }

                        // y axis
                        if (this.pidAxisOnOff[1])
                        {
                            SolidColorBrush dd = new SolidColorBrush();
                            dd.Color = Colors.Green;
                            this._mainCtrl._pidMonitor.btnYonoff.Background = dd;
                            this._mainCtrl._pidMonitor.btnYonoff.Content = "PID On";
                            this._mainCtrl._pidMonitor.pidYStatus = true;
                        }
                        else
                        {
                            SolidColorBrush dd = new SolidColorBrush();
                            dd.Color = Colors.Red;
                            this._mainCtrl._pidMonitor.btnYonoff.Background = dd;
                            this._mainCtrl._pidMonitor.btnYonoff.Content = "PID Off";
                            this._mainCtrl._pidMonitor.pidYStatus = false;
                        }

                        // z axis
                        if (this.pidAxisOnOff[2])
                        {
                            SolidColorBrush dd = new SolidColorBrush();
                            dd.Color = Colors.Green;
                            this._mainCtrl._pidMonitor.btnZonoff.Background = dd;
                            this._mainCtrl._pidMonitor.btnZonoff.Content = "PID On";
                            this._mainCtrl._pidMonitor.pidZStatus = true;
                        }
                        else
                        {
                            SolidColorBrush dd = new SolidColorBrush();
                            dd.Color = Colors.Red;
                            this._mainCtrl._pidMonitor.btnZonoff.Background = dd;
                            this._mainCtrl._pidMonitor.btnZonoff.Content = "PID Off";
                            this._mainCtrl._pidMonitor.pidZStatus = false;
                        }
                        #endregion

                    });
                    this.updatePidOnOffStatus = false;
                }
            }


            #endregion


            this._main.Dispatcher.Invoke(() =>
            {
                this._main.listInfoType.Items.Clear();
            });

            List<string> temp = setInfoTypes.ToList<string>();
            for (int i = 0; i < setInfoTypes.Count; i++)
            {
                #region dataTypesMainMonitor
                // updating incoming data types in main monitor
                temp.Sort();
                if (temp[i].Equals("a"))
                {
                    this._main.Dispatcher.Invoke(() =>
                    {
                        this._main.listInfoType.Items.Add("a: Accelerometer");
                    });
                }
                else if (temp[i].Equals("g"))
                {
                    this._main.Dispatcher.Invoke(() =>
                    {
                        this._main.listInfoType.Items.Add("g: Gyroscope");
                    });

                }
                else if (temp[i].Equals("c"))
                {
                    this._main.Dispatcher.Invoke(() =>
                    {
                        this._main.listInfoType.Items.Add("c: Complementary Filter");
                    });

                }
                else if (temp[i].Equals("m"))
                {
                    this._main.Dispatcher.Invoke(() =>
                    {
                        this._main.listInfoType.Items.Add("m: Motor PWM");
                    });

                }

                else if (temp[i].Equals("p"))
                {

                    this._main.Dispatcher.Invoke(() =>
                    {
                        this._main.listInfoType.Items.Add("p: PID");
                    });

                #endregion

                }
                temp.Clear();


                setInfoTypes.Clear();

            #endregion
            }
        }

        private void updateMain(string[] gyro, string[] acc, string[] cFilter, string[] motor, string[] mag, string distanceToGround, string[] localCoord, string[] ctrlReference)
        {

            #region for loop calculating attitudes

            //double norm, xRaw, yRaw, zRaw;
            //// this for loop calculates variables.
            //for (int i = 0; i < 3; i++)
            //{

            //    try 
            //    {
            //        if (i == 0) // gyro
            //        {
            //            xRaw = Double.Parse(gyro[0]);
            //            yRaw = Double.Parse(gyro[1]);
            //            zRaw = Double.Parse(gyro[2]);
            //            norm = Math.Sqrt(xRaw * xRaw + yRaw * yRaw + zRaw * zRaw);
            //            gyro[3] = norm.ToString();
            //            gyro[4] = (xRaw / norm).ToString();
            //            gyro[5] = (Math.Acos(xRaw / norm) * 180 / Math.PI).ToString();
            //            gyro[6] = (yRaw / norm).ToString();
            //            gyro[7] = (Math.Acos(yRaw / norm) * 180 / Math.PI).ToString();
            //            gyro[8] = (zRaw / norm).ToString();
            //            gyro[9] = (Math.Acos(zRaw / norm) * 180 / Math.PI).ToString();

            //        }
            //        else if (i == 1) //acc
            //        {
            //            xRaw = Double.Parse(acc[0]);
            //            yRaw = Double.Parse(acc[1]);
            //            zRaw = Double.Parse(acc[2]);
            //            norm = Math.Sqrt(xRaw * xRaw + yRaw * yRaw + zRaw * zRaw);

            //            //if (norm != 0)
            //            //{
            //            //    acc[3] = norm.ToString().Substring(0, 6);
            //            //    acc[4] = (xRaw / norm).ToString().Substring(0, 6);
            //            //    acc[5] = (Math.Acos(xRaw / norm) * 180 / Math.PI).ToString().Substring(0, 6);
            //            //    acc[6] = (yRaw / norm).ToString().Substring(0, 6);
            //            //    acc[7] = (Math.Acos(yRaw / norm) * 180 / Math.PI).ToString().Substring(0, 6);
            //            //    acc[8] = (zRaw / norm).ToString().Substring(0, 6);
            //            //    acc[9] = (Math.Acos(zRaw / norm) * 180 / Math.PI).ToString().Substring(0, 6);
            //            //}
            //        }
            //        else if (i==2)  // compFilter
            //        {
            //            xRaw = Double.Parse(cFilter[0]);
            //            yRaw = Double.Parse(cFilter[1]);
            //            zRaw = Double.Parse(cFilter[2]);
            //            norm = 100*Math.Sqrt(xRaw * xRaw/10000 + yRaw * yRaw/10000 + zRaw * zRaw/10000);
            //            //if (norm > 8500 || norm < 8000)
            //            //{
            //            //    Console.WriteLine("Cx = " + xRaw);
            //            //    Console.WriteLine("Cy = " + yRaw);
            //            //    Console.WriteLine("Cz = " + zRaw);
            //            //    Console.WriteLine("norm = " + norm);
            //            //}
            //            if (norm != 0)
            //            {
            //                cFilter[3] = norm.ToString();
            //                cFilter[4] = (xRaw / norm).ToString();
            //                cFilter[5] = (Math.Acos(xRaw / norm) * 180 / Math.PI).ToString();
            //                cFilter[6] = (yRaw / norm).ToString();
            //                cFilter[7] = (Math.Acos(yRaw / norm) * 180 / Math.PI).ToString();
            //                cFilter[8] = (zRaw / norm).ToString();
            //                cFilter[9] = (Math.Acos(zRaw / norm) * 180 / Math.PI).ToString();
            //            }

            //        }
            //    }catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //        Console.WriteLine("acc[0]=" + acc[0]);
            //        Console.WriteLine("acc[1]=" + acc[1]);
            //        Console.WriteLine("acc[2]=" + acc[2]);
            //        Console.WriteLine("gyro[0]=" + gyro[0]);
            //        Console.WriteLine("gyro[0]=" + gyro[1]);
            //        Console.WriteLine("gyro[0]=" + gyro[2]);
            //        Console.WriteLine("cFilter[0]=" + cFilter[0]);
            //        Console.WriteLine("cFilter[0]=" + cFilter[1]);
            //        Console.WriteLine("cFilter[0]=" + cFilter[2]);

            //    }

            //}
            #endregion

            #region for collection mode

            double avgTimeElapse=0;   
             
            if (this._collectionMode)
            {
                sw.Stop();
                //sw.Reset();
                _numberOfDataGathered++;
                double a = sw.ElapsedMilliseconds;
                Console.WriteLine(a);
                //sw.Reset();
                sw.Start();
                Console.WriteLine("gathering...");
                //Console.WriteLine("asdfasfsdfasdfsdfds" + this.throttle[0]);

                if (_numberOfDataGathered > 0)
                {
                    avgTimeElapse = a / 1000 / _numberOfDataGathered;
                }

                this._main.Dispatcher.Invoke(() =>
                {

                    if (this._numberOfDataGathered > 0)
                    {
                        this._main.txtNumberOfData.Text = this._numberOfDataGathered.ToString() + " data gathered.";
                        this._main.lblBoard.Content = "avgTimeElapse = " + avgTimeElapse.ToString().Substring(0, 5) + " sec";
                    }

                });



                this._data.Throttle.Add(this.throttle[0]);
                

                for (int i = 0; i < 10; i++)
                {

                    this._data.gyro[i].Add(gyro[i]);
                    this._data.acc[i].Add(acc[i]);
                    this._data.cFilter[i].Add(cFilter[i]);
                    if (i < 3)
                    {
                        this._data.mag[i].Add(mag[i]);
                        this._data.ctrlRefAtt[i].Add(ctrlReference[i]);
                    }
                }


            }
            else
            {
                this._numberOfDataGathered = 0;
            }

            #endregion

            #region update main window

            this._main.Dispatcher.Invoke(() =>
            {
                // control reference angle

                this._main.txtCtrlRoll.Text = (Double.Parse(ctrlReference[0])/100).ToString();
                this._main.txtCtrlPitch.Text = (Double.Parse(ctrlReference[1]) / 100).ToString();
                this._main.txtCtrlYaw.Text = (Double.Parse(ctrlReference[2]) / 100).ToString();


                //distance to ground
                //if (!distanceToGround.Equals("0"))
                //{
                    this._main.txtDist2Gnd.Text = distanceToGround;
                //}
                // localCord update
                //if (!localCoord[0].Equals("0")) this._main.txtLocalCoordX.Text = localCoord[0];
                //if (!localCoord[1].Equals("0")) this._main.txtLocalCoordY.Text = localCoord[1];
                //if (!localCoord[2].Equals("0")) this._main.txtLocalCoordZ.Text = localCoord[2];
                this._main.txtLocalCoordX.Text = localCoord[0];
                this._main.txtLocalCoordY.Text = localCoord[1];
                this._main.txtLocalCoordZ.Text = localCoord[2];


                // gyro update
                //for (int i = 0; i < 10; i++)
                for (int i = 0; i < 3; i++)
                {
                    this._main.gyro[i].Text = gyro[i];
                }

                // acc update
                //for (int i = 0; i < 10; i++)
                if(!acc[0].Equals("0") && !acc[1].Equals("0") && !acc[2].Equals("0"))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (acc[i].Equals("0"))
                        {
                            Console.WriteLine(instreamGlobalVar);
                            Console.WriteLine();
                        }
                        this._main.acc[i].Text = acc[i];
                    }
                }
                        

                // magnetometer update
                double x, y, z;
                x = double.Parse(mag[0]);
                y = double.Parse(mag[1]);
                z = double.Parse(mag[2]);
                x /= 172;
                y /= 172;
                z /= 172;


                this._main.txtHeadingX.Text = mag[0];
                this._main.txtHeadingY.Text = mag[1];
                this._main.txtHeadingZ.Text = mag[2];

                //this._main.txtHeadingX.Text = x.ToString();
                //this._main.txtHeadingY.Text = y.ToString();
                //this._main.txtHeadingZ.Text = z.ToString();
                //double head = Math.Atan2(double.Parse(mag[1]), double.Parse(mag[0]))*180/Math.PI;
                //this._main.txtHeading.Text = head.ToString();


                // comp. filter update
                for (int i = 0; i < 10; i++)
                {
                    if (!cFilter[i].Equals("0"))
                    {
                        double temp = Double.Parse(cFilter[i]);
                        temp = temp / 100.00;

                        this._main.cFilter[i].Text = temp.ToString();

                    }
                    else
                    {
                        break;
                    }
                }


                //motor update
                if (!motor[0].Equals("0"))
                {
                    this._main.txtMotor1.Text = motor[0];
                }
                if (!motor[1].Equals("0"))
                {
                    this._main.txtMotor2.Text = motor[1];
                }
                if (!motor[2].Equals("0"))
                {
                    this._main.txtMotor3.Text = motor[2];
                }
                if (!motor[3].Equals("0"))
                {
                    this._main.txtMotor4.Text = motor[3];
                }
                if (!motor[4].Equals("0"))
                {
                    this._main.txtMotor5.Text = motor[4];
                }
                if (!motor[5].Equals("0"))
                {
                    this._main.txtMotor6.Text = motor[5];
                }

                this._main.txtMainThrottle.Text = throttle[0];


                this._mainCtrl.updateSlider();



                
                    
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
                    Console.WriteLine("Check usb connection");
                    ex.GetType();
                }
                
            }

        #endregion


    }
}
