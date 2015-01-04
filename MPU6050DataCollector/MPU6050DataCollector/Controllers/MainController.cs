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
            this.setDataAddress();
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

        

        private String getDataFolderAddr()
        {
            // get VocabList Folder's address
            String dir = Environment.CurrentDirectory;
            // dir end with "Release or Debug" as long as development going on
            String check = dir.Substring(dir.Length - 7, 7);
            if (check.Equals("Release"))
            {
                // length(bin\Release) = 11
                dir = dir.Substring(0, dir.Length - 11) + "Data\\";
            }
            else
            {
                // length(bin\Debug) = 9
                dir = dir.Substring(0, dir.Length - 9) + "Data\\";
            }
            return dir;
        }
        
        public void setDataAddress()
        {
            this._main.lblAddress.Content = this.getDataFolderAddr();
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
            Excel.Worksheet[] sheet = new Excel.Worksheet[3];
            object misValue = System.Reflection.Missing.Value;

            xlBook = xlFile.Workbooks.Add(misValue);
            
            int outterIteration = this._data.gyro.Length;
            int innerIteration = this._data.gyro[0].Count;
          
            // sheet one - gyro
            sheet[0] = (Excel.Worksheet)xlBook.Worksheets.get_Item(1);
            sheet[0].Name = (string) "Gyroscope";
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
                    sheet[0].Cells[j + 2, i + 1] = temp[j];
                }
            }
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


            xlBook.SaveAs(this._main.lblAddress.Content + this._main.txtFileName.Text);
            xlBook.Close(true, misValue, misValue);
            xlFile.Quit();

            releaseObject(sheet[0]);
            releaseObject(sheet[1]);
            releaseObject(sheet[2]);
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


    }
}
