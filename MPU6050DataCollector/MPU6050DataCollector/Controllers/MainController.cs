/*============================================================
// Made by: Elliot Hongyun Lee
// Undergrad Research Project
============================================================*/

using MPU6050DataCollector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;
using System.Windows;
using SerialMonitorTest03.ControllerFolder;
using Excel = Microsoft.Office.Interop.Excel;
using WpfApplication1;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.ComponentModel;


namespace MPU6050DataCollector.Controllers
{
    class MainController
    {
        private MainWindow _main;
        private AttitudeData _data;
        private USB _usb;
        private bool _usbConnected;
        private LineAttitude lineX, lineY, lineZ;
        private bool[] _pidOnOff = {false, false, false};

        internal AttPIDMonitor _attPidMonitor= null;
        internal NavPIDMonitor _navPidMonitor = null;
        internal JoyStickController _joyStick = null;
        internal InfoChoiceMonitor _infoChoiceMonitor = null;

        public bool _attPidMonitorIsOpen = false;
        public bool _joyStickIsOpen = false;
        public bool _navPidMonitorIsOpen = false;
        public bool _infoChoiceMonitorIsOpen = false;

        private ImageBrush bg3;

        private bool SDStatus;

        public MainController(MainWindow x, AttitudeData y)
        {
            this._main = x;
            this._data = y;
            this._usb = new USB(x, y, this);
            this._usbConnected = false;
            this.setDataAddress();
            this.refreshComports();
            this.SDStatus = false;
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
            this._main.comboBaudRate.Items.Insert(0, 57600);
            //this._main.comboBaudRate.Items.Insert(0, 38400);

            if (this._usb.numberOfPorts > 0)
            {
                
                this._main.comboPorts.SelectedIndex = 0;
                this._main.comboBaudRate.SelectedIndex = 0;
            }

            this.keepConnectionButtonConvention();
        }

        #endregion

        public String InfoChoice
        {
            get { return _usb.InfoChoice; }
        }


        public bool pidOnOffX
        {
            set { this._pidOnOff[0]= value;}
            get { return this._pidOnOff[0]; }
        }
        public bool pidOnOffY
        {
            set { this._pidOnOff[1] = value; }
            get { return this._pidOnOff[1]; }
        }
        public bool pidOnOffZ
        {
            set { this._pidOnOff[2] = value; }
            get { return this._pidOnOff[2]; }
        }
        
        public void setDataAddress()
        {
            this._main.lblAddress.Content = AbsAddress.getFolderAddr("Data");
            this._main.txtFileName.Text = "data01.xlsx";
        }

        public void saveToExcel()
        {
            Console.WriteLine("Saving...");
            Excel.Application xlFile = new Microsoft.Office.Interop.Excel.Application();

            if (xlFile == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            Excel.Workbook xlBook;
            Excel.Worksheet[] sheet = new Excel.Worksheet[5];
            object misValue = System.Reflection.Missing.Value;

            xlBook = xlFile.Workbooks.Add(misValue);

            int outterIteration = this._data.gyro.Length;
            int innerIteration = this._data.gyro[0].Count;
            
            // sheet one - gyro
            sheet[0] = (Excel.Worksheet)xlBook.Worksheets.get_Item(1);
            sheet[0].Name = (string)"Gyroscope";
            sheet[0].Cells[1, 1] = "RawX";
            sheet[0].Cells[1, 2] = "RawY";
            sheet[0].Cells[1, 3] = "RawZ";
            sheet[0].Cells[1, 4] = "Normal";
            sheet[0].Cells[1, 5] = "DircosX";
            sheet[0].Cells[1, 6] = "Degreex";
            sheet[0].Cells[1, 7] = "DirCosY";
            sheet[0].Cells[1, 8] = "DegreeY";
            sheet[0].Cells[1, 9] = "DircosZ";
            sheet[0].Cells[1, 10] = "DegreeZ";
           
            for (int i = 0; i < outterIteration; i++)
            {
                List<string> temp = this._data.gyro[i];
                
                for (int j = 0; j < innerIteration; j++)
                {
                    //Console.WriteLine("asfasdf");
                    //Console.WriteLine(i);

                    sheet[0].Cells[j + 2, i + 1] = temp[j];
                }
                
            }
            //Console.WriteLine("2");
            // sheet two - acc
            sheet[1] = (Excel.Worksheet)xlBook.Worksheets.Add();
            sheet[1].Name = (string)"Accelerometer";
            sheet[1].Cells[1, 1] = "RawX";
            sheet[1].Cells[1, 2] = "RawY";
            sheet[1].Cells[1, 3] = "RawZ";
            sheet[1].Cells[1, 4] = "Normal";
            sheet[1].Cells[1, 5] = "DircosX";
            sheet[1].Cells[1, 6] = "Degreex";
            sheet[1].Cells[1, 7] = "DirCosY";
            sheet[1].Cells[1, 8] = "DegreeY";
            sheet[1].Cells[1, 9] = "DircosZ";
            sheet[1].Cells[1, 10] = "DegreeZ";
            for (int i = 0; i < outterIteration; i++)
            {
                List<string> temp = this._data.acc[i];
          
                for (int j = 0; j < innerIteration; j++)
                {
                    sheet[1].Cells[j + 2, i + 1] = temp[j];
                }
                
            }
            
            // sheet three - cFilter
            sheet[2] = (Excel.Worksheet)xlBook.Worksheets.Add();
            sheet[2].Name = (string)"cFilter";
            sheet[2].Cells[1, 1] = "RawX";
            sheet[2].Cells[1, 2] = "RawY";
            sheet[2].Cells[1, 3] = "RawZ";
            sheet[2].Cells[1, 4] = "Normal";
            sheet[2].Cells[1, 5] = "DircosX";
            sheet[2].Cells[1, 6] = "Degreex";
            sheet[2].Cells[1, 7] = "DirCosY";
            sheet[2].Cells[1, 8] = "DegreeY";
            sheet[2].Cells[1, 9] = "DircosZ";
            sheet[2].Cells[1, 10] = "DegreeZ";
            for (int i = 0; i < outterIteration; i++)
            {
                List<string> temp = this._data.cFilter[i];
                
                for (int j = 0; j < innerIteration; j++)
                {
                    sheet[2].Cells[j + 2, i + 1] = temp[j];
                }
                
            }
            // sheet four - magnetometer
            sheet[3] = (Excel.Worksheet)xlBook.Worksheets.Add();
            sheet[3].Name = (string)"Magnetometer";
            sheet[3].Cells[1, 1] = "MagX";
            sheet[3].Cells[1, 2] = "MagY";
            sheet[3].Cells[1, 3] = "MagZ";
            for (int i = 0; i < 3; i++)
            {
                List<string> temp = this._data.mag[i];
                
                for (int j = 0; j < innerIteration; j++)
                {
                    sheet[3].Cells[j + 2, i + 1] = temp[j];
                }
                
            }
            // sheet fives - control ref
            sheet[4] = (Excel.Worksheet)xlBook.Worksheets.Add();
            sheet[4].Name = (string)"Attitude Reference";
            sheet[4].Cells[1, 1] = "pitch";
            sheet[4].Cells[1, 2] = "roll";
            sheet[4].Cells[1, 3] = "yaw";
            sheet[4].Cells[1, 5] = "Throttle";
            
            for (int i = 0; i < 3; i++)
            {
                List<string> temp = this._data.ctrlRefAtt[i];
                
                for (int j = 0; j < innerIteration; j++)
                {
                    sheet[4].Cells[j + 2, i + 1] = temp[j];
                    if (i < 1)
                    {
                        List<string> throt = this._data.Throttle;
                        sheet[4].Cells[j + 2, 5] = throt[j];
                    }
                }
            }




            xlBook.SaveAs(this._main.lblAddress.Content + this._main.txtFileName.Text);
            xlBook.Close(true, misValue, misValue);
            xlFile.Quit();

            releaseObject(sheet[0]);
            releaseObject(sheet[1]);
            releaseObject(sheet[2]);
            releaseObject(sheet[3]);
            releaseObject(sheet[4]);
            releaseObject(xlBook);
            releaseObject(xlFile);

            MessageBox.Show("Excel file created , you can find the file " + this._main.lblAddress.Content + this._main.txtFileName.Text);
            this._data = new AttitudeData();
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public void startCollecting()
        {
            if (this._usb.collectionMode)
            {
                this._usb.collectionMode = false;
                this._main.btnCollect.Content = "Collecting Data";
            }
            else
            {
                this._usb.collectionMode = true;
                this._main.btnCollect.Content = "Stop collecting";
            }
        }

        public void requestInfo(string val)
        {
            this._usb.sendDataRobust(val);
        }


        //public void preUpdatePidConst(int kType, int direction)
        //{
        //    if (direction < 0)
        //    {
        //        if (kType == 1)
        //        {
        //            this._main.txtKp.Text = (int.Parse(this._main.txtKp.Text) - 1).ToString();
        //        }
        //        else if (kType == 2)
        //        {
        //            this._main.txtKi.Text = (int.Parse(this._main.txtKi.Text) - 1).ToString();
        //        }
        //        else if (kType == 3)
        //        {
        //            this._main.txtKd.Text = (int.Parse(this._main.txtKd.Text) - 1).ToString();
        //        }
        //    }
        //    else if (direction > 0)
        //    {
        //        if (kType == 1)
        //        {
        //            this._main.txtKp.Text = (int.Parse(this._main.txtKp.Text) + 1).ToString();
        //        }
        //        else if (kType == 2)
        //        {
        //            this._main.txtKi.Text = (int.Parse(this._main.txtKi.Text) + 1).ToString();
        //        }
        //        else if (kType == 3)
        //        {
        //            this._main.txtKd.Text = (int.Parse(this._main.txtKd.Text) + 1).ToString();
        //        }
        //    }

        //    updatePidConst();
        //}


        public async void updateAttPidAttConst(string[] k)
        {
            
            updateAttPidConstX(k);
            await Task.Delay(500);
            updateAttPidConstY(k);
            await Task.Delay(500);
            updateAttPidConstZ(k);
        }
        
        public void updateAttPidConstX(string[] k)
        {
            if (k[0].Equals("") || k[1].Equals("") || k[2].Equals(""))
            {
                MessageBox.Show("Check the connection and request propeller's pid constant");
                return;
            }
            this._usb.sendDataRobust("pA" + k[0]);
            this._usb.sendDataRobust("pB" + k[1]);
            this._usb.sendDataRobust("pC" + k[2]);

        }
        public void updateAttPidConstY(string[] k)
        {

            if (k[3].Equals("") || k[4].Equals("") || k[5].Equals(""))
            {
                MessageBox.Show("Check the connection and request propeller's pid constant");
                return;
            }

            this._usb.sendDataRobust("pD" + k[3]);
            this._usb.sendDataRobust("pE" + k[4]);
            this._usb.sendDataRobust("pF" + k[5]);
        }
        public void updateAttPidConstZ(string[] k)
        {

            if (k[6].Equals("") || k[7].Equals("") || k[8].Equals(""))
            {
                MessageBox.Show("Check the connection and request propeller's pid constant");
                return;
            }

            this._usb.sendDataRobust("pG" + k[6]);
            this._usb.sendDataRobust("pH" + k[7]);
            this._usb.sendDataRobust("pI" + k[8]);

        }


        public async void updateNavPidAttConst(string[] k)
        {

            updateNavPidConstX(k);
            await Task.Delay(500);
            updateNavPidConstY(k);
            await Task.Delay(500);
            updateNavPidConstZ(k);
        }


        public void updateNavPidConstX(string[] k)
        {
            this._usb.sendDataRobust("PA" + k[0]);
            this._usb.sendDataRobust("PB" + k[1]);
            this._usb.sendDataRobust("PC" + k[2]);
        }


        public void updateNavPidConstY(string[] k)
        {
            this._usb.sendDataRobust("PD" + k[3]);
            this._usb.sendDataRobust("PE" + k[4]);
            this._usb.sendDataRobust("PF" + k[5]);
        }

        public void updateNavPidConstZ(string[] k)
        {

            this._usb.sendDataRobust("PG" + k[6]);
            this._usb.sendDataRobust("PH" + k[7]);
            this._usb.sendDataRobust("PI" + k[8]);
            this._usb.sendDataRobust("PJ" + k[9]);
            this._usb.sendDataRobust("PK" + k[10]);
        }

        public void prepareMode()
        {
            this._usb.sendDataRobust("D1");
            Console.WriteLine("System Mode = prepare. Message sent: D1");
        }
        public void preTakeOffMode()
        {
            this._usb.sendDataRobust("D2");
            Console.WriteLine("System Mode = pre Take-off. Message sent: D2");
        }

        public void takeOffMode()
        {
            this._usb.sendDataRobust("D3");
            Console.WriteLine("System Mode = take off. Message sent: D3");
        }

        public void idleMode()
        {
            this._usb.sendDataRobust("D0");
            Console.WriteLine("System Mode = prepare. Message sent: D0");
        }

        
        public void requestAttPidOnOffStatus()
        {
            string command = "R12";
            this._usb.sendDataRobust(command);
        }

        public  void requestAttPidConst()
        {
            // R11 = request PID contants like Kp, Ki, Kd
            string command = "R11";
            this._usb.sendDataRobust(command);
            Console.WriteLine("PID Constant requested: " + command);
        }


        public void requestNavPidOnOffStatus()
        {
            string command = "R3";
            this._usb.sendDataRobust(command);
        }

        public void requestNavPidConst()
        {
            string command = "R2";
            this._usb.sendDataRobust(command);
            //Console.WriteLine("NavPID on off status requested: " + command);
        }



        public void requestInfoChoice()
        {
            string command = "R1";
            this._usb.sendDataRobust(command);
        }

        public void sendInfoChoice(string val)
        {
            string command = "I" + val;
            this._usb.sendDataRobust(command);
        }


        internal void Loaded()
        {
            ImageBrush bg = new ImageBrush();
            bg.ImageSource = new BitmapImage(new Uri(AbsAddress.getFolderAddr("Resource") + "coord.jpg"));
            this._main.canvasAttitudeX.Background = bg;
            this._main.canvasAttitudeY.Background = bg;

            ImageBrush bg2 = new ImageBrush();
            bg2.ImageSource = new BitmapImage(new Uri(AbsAddress.getFolderAddr("Resource") + "HeadingWeel.bmp"));
            this._main.canvasHeading.Background = bg2;

            bg3 = new ImageBrush();
            bg3.ImageSource = new BitmapImage(new Uri(AbsAddress.getFolderAddr("Resource") + "HeadingIndicator_Aircraft.bmp"));
            this._main.canvasHeading_indicator.Background = bg3;

            this.lineX = new LineAttitude(this._main.canvasAttitudeX);
            this.lineY = new LineAttitude(this._main.canvasAttitudeY);
            this.lineZ = new LineAttitude(this._main.canvasHeading);

            this.setSlider();


        }

        internal void updateAttitudeX(string deg)
        {
            
            double angle = Double.Parse(deg);
            this._main.canvasAttitudeX.Children.Clear();
            this._main.canvasAttitudeX.Children.Add(lineX.getLine(angle));
            //Console.WriteLine("deg: " + deg);
        }

        internal void updateAttitudeY(string deg)
        {

            double angle = Double.Parse(deg);
            this._main.canvasAttitudeY.Children.Clear();
            this._main.canvasAttitudeY.Children.Add(lineY.getLine(angle));
            //Console.WriteLine("deg: " + deg);
        }

        internal void updateAttitudeZ(string deg)
        {
            double rotationAngle = Double.Parse(deg);
            //rotationAngle /= 100;
            var transform = Matrix.Identity;
            transform.RotateAt(rotationAngle, 0.5, 0.5);
            bg3.RelativeTransform = new MatrixTransform(transform);

            this._main.canvasHeading_indicator.Background = bg3;
        }

        public void updateSlider()
        {
            try
            {
                this._main.moterSlide1.Value = Double.Parse(this._main.txtMotor1.Text);
                this._main.motorSlide2.Value = Double.Parse(this._main.txtMotor2.Text);
                this._main.motorSlide3.Value = Double.Parse(this._main.txtMotor3.Text);
                this._main.motorSlide4.Value = Double.Parse(this._main.txtMotor4.Text);
                this._main.motorSlide5.Value = Double.Parse(this._main.txtMotor5.Text);
                this._main.motorSlide6.Value = Double.Parse(this._main.txtMotor6.Text);
                this._main.sldrMainThrottle.Value = Double.Parse(this._main.txtMainThrottle.Text);
            }
            catch (Exception ee)
            {
                ee.GetType();
            }
            
        }

        public void setSlider()
        {
            this._main.moterSlide1.Minimum = 1100;
            this._main.motorSlide2.Minimum = 1100;
            this._main.motorSlide3.Minimum = 1100;
            this._main.motorSlide4.Minimum = 1100;
            this._main.motorSlide5.Minimum = 1100;
            this._main.motorSlide6.Minimum = 1100;
            this._main.sldrMainThrottle.Minimum = 1100;

            this._main.moterSlide1.Maximum = 2500;
            this._main.motorSlide2.Maximum = 2500;
            this._main.motorSlide3.Maximum = 2500;
            this._main.motorSlide4.Maximum = 2500;
            this._main.motorSlide5.Maximum = 2500;
            this._main.motorSlide6.Maximum = 2500;
            this._main.sldrMainThrottle.Maximum = 2500;
        }
        
        public void decrease(int motor, string currentPwm, int delta)
        {
            // motor : from 1 to 6
            //int newPwm = int.Parse(currentPwm) - delta;
            int newPwm =  -delta;
            String motorNumber="x";
            if (motor == 1) motorNumber = "a";
            else if(motor==2) motorNumber = "b";
            else if (motor == 3) motorNumber = "c";
            else if (motor == 4) motorNumber = "d";
            else if (motor == 5) motorNumber = "e";
            else if (motor == 6) motorNumber = "f";

          
            this._usb.sendData("M" + motorNumber + newPwm.ToString());
           

            
            //Console.WriteLine("M" + motorNumber + newPwm.ToString() + ";");
        }

        public void increaseAll(int delta)
        {
            String motorNumber = "g";
            
            this._usb.sendData("M" + motorNumber + delta.ToString());
               
            
        }
        public  void decreaseAll(int delta)
        {
            String motorNumber = "g";

            this._usb.sendData("M" + motorNumber +"-"+ delta.ToString());
                
            
        }
        public  void increase(int motor, string currentPwm, int delta)
        {
            //int newPwm = int.Parse(currentPwm) + delta;
            int newPwm = delta;
            String motorNumber = "x";
            if (motor == 1) motorNumber = "a";
            else if (motor == 2) motorNumber = "b";
            else if (motor == 3) motorNumber = "c";
            else if (motor == 4) motorNumber = "d";
            else if (motor == 5) motorNumber = "e";
            else if (motor == 6) motorNumber = "f";

           
           this._usb.sendData("M" + motorNumber + newPwm.ToString());
            
            //Console.WriteLine("M" + motorNumber + newPwm.ToString() + ";");
        }

        public void testSend()
        {
            //_usb.sendTestSD();
            
            
        }

        public void SDRecording()
        {
            if (!SDStatus)
            {
                SDStatus = true;
                //send B1;
                _usb.sendDataRobust("B1");
                this._main.btnSD.Content = "Stop SD Recording";
                MessageBox.Show("SD on sent");
            }
            else
            {
                SDStatus = false;
                _usb.sendDataRobust("B0");
                this._main.btnSD.Content = "Start SD Recording";
                MessageBox.Show("SD off sent");
            }
        }

        public void setBatteryBar(string[] voltage)
        {
            double[] cell = new double[6];
            this._main.Dispatcher.Invoke(() =>
            {
                
                for (int i=0; i< 6; i++)
                {
                    cell[i] = double.Parse(voltage[i]);
                    if (cell[i] > 85)  // green over 3.67
                    {
                        this._main.CellBar[i].Foreground = new SolidColorBrush(Colors.Green);
                    }else if(cell[i] > 80) // yellow for 3.45 ~ 3.66
                    {
                        this._main.CellBar[i].Foreground = new SolidColorBrush(Colors.Yellow);
                    }
                    else // red below 3.44v
                    {
                        this._main.CellBar[i].Foreground = new SolidColorBrush(Colors.Red);
                        Console.WriteLine("=================");
                        Console.WriteLine(cell[i]);
                    }

                    this._main.CellBar[i].Value = cell[i];
                    this._main.CellInfo[i].Text = voltage[i];
                        
                }
            });



        }


        public void openNavPIDMonitor()
        {
            if(!_navPidMonitorIsOpen)
            {
                openInfoChoiceMonitor();
                _infoChoiceMonitor.autoSetPidNavDebugMode();
                this._navPidMonitor = NavPIDMonitor.Instance();
                this._navPidMonitor.MainCtrl = this;
                this._navPidMonitor.Closing += closeNavPIDMonitor;
                this._navPidMonitorIsOpen = true;
                this._navPidMonitor.Show();

                this.requestNavPidOnOffStatus();
                this.requestNavPidConst();
            }
            
        }
        public void closeNavPIDMonitor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this._navPidMonitorIsOpen = false;
            openInfoChoiceMonitor();
            _infoChoiceMonitor.autoClosePidNavDebugMode();
        }

        public void openAttPIDMonitor()
        {
            if (!_attPidMonitorIsOpen)
            {
                openInfoChoiceMonitor();
                _infoChoiceMonitor.autoSetPidAttDebugMode();
                this._attPidMonitor = AttPIDMonitor.Instance();
                this._attPidMonitor.MainCtrl = this;
                this._attPidMonitor.Closing += closeAttPIDMonitor;
                this._attPidMonitorIsOpen = true;
                this._attPidMonitor.Show();

                this.requestAttPidOnOffStatus();
                this.requestAttPidConst();
            }
        }

        public void closeAttPIDMonitor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            openInfoChoiceMonitor();
            _infoChoiceMonitor.autoClosePidAttDebugMode();
            this._attPidMonitorIsOpen = false;
            
        }

        public void openJoyStick()
        {
            if (!_joyStickIsOpen)
            {
                this._joyStick = JoyStickController.Instance;//new JoyStickController(this, this._usb.GetControlRefYaw);

                this._joyStick.setUp(this, this._usb.GetControlRefYaw);
                this._joyStick.Closing += new CancelEventHandler(closeJoyStick);
                this._joyStickIsOpen = true;
                this._joyStick.Show();


                //this.requestPidOnOffStatus();
                //this.requestPidConst();


            }
        }

        public void closeJoyStick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this._joyStickIsOpen = false;

        }

        public void openInfoChoiceMonitor()
        {
            if (!_infoChoiceMonitorIsOpen)
            {
                this._infoChoiceMonitor = InfoChoiceMonitor.Instance();
                this._infoChoiceMonitor.MainCtrl = this;
                this._infoChoiceMonitor.Closing += closeInfoChoiceMonitor;
                this._infoChoiceMonitorIsOpen = true;
                this._infoChoiceMonitor.Show();
            }
        }

        public void closeInfoChoiceMonitor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this._infoChoiceMonitorIsOpen = false;

        }


        public void updateThrottle(int val)
        {
            string msg = "TH" + val.ToString();
            this._usb.sendDataRobust(msg);
            Console.WriteLine(val);

        }

        public void updateRefAttPitch(double p)
        {
            int pitch = (int)( p * 100);
            

            String msg1 = "CA" + pitch.ToString();
                             
            this._usb.sendData(msg1);
            
            Console.WriteLine(msg1);

        }

        public void updateRefAttRoll( double r)
        {
            
            int roll = (int) (r * 100);
            String msg2 = "CB" + roll.ToString();
            this._usb.sendData(msg2);
            Console.WriteLine(msg2);


        }

        public void updateRefAttYaw(double y)
        {
            int yaw = (int) (y*100);

            String msg3 = "CC" + yaw.ToString();
            
            this._usb.sendData(msg3);
            Console.WriteLine(msg3);

        }

    }
}

