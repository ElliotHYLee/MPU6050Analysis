using SerialMonitorTest03.ControllerFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MPU6050DataCollector.Monitors.PID
{
    public partial class PIDAnalyzer : Form
    {

        private Thread pitchPlotterThread;

        USB _usbObject = null;
        bool isOn;
        static private PIDAnalyzer _instance = null;
        public PIDAnalyzer()
        {
            InitializeComponent();
            isOn = true;
            this.FormClosing += close;
        }

        internal  USB setUSBObject
        {
            set { _usbObject = value; }
        }

        static public PIDAnalyzer Instance
        {
            get
            {
                if (_instance == null) _instance = new PIDAnalyzer();
                return _instance;
            }
        }

        private void close(object sender, FormClosingEventArgs e)
        {
            isOn = false;
            _instance = null;
        }

        private void PIDAnalizer_Load(object sender, EventArgs e)
        {

        }

        private void btnPitchSP_Click(object sender, EventArgs e)
        {

        }

        private void btnPitchStart_Click(object sender, EventArgs e)
        {
            pitchPlotterThread = new Thread(new ThreadStart(this.plotPitch));
            pitchPlotterThread.IsBackground = true;
            pitchPlotterThread.Start();
            //MessageBox.Show("asfdasdf");
        }

        private void plotPitch()
        {
            double[] pitchArr;
            while (true)
            {
                pitchArr = _usbObject.RecentPitch.ToArray();
                if (chPitch.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { updateChart(chPitch, pitchArr, 1400); });
                }
                Thread.Sleep(1000 / 5);
            }
        }

        private void plotRoll()
        {
            double[] rollArr;
            while (true)
            {
                rollArr = _usbObject.RecentRoll.ToArray();
                if (chPitch.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { updateChart(chPitch, rollArr, 0); });
                }
                Thread.Sleep(1000 / 5);
            }
        }


        private void updateChart(Chart ch, double[] arr, double input)
        {
            ch.Series["Series1"].Points.Clear();
            ch.Series["Series2"].Points.Clear();

            for (int i= 0; i< arr.Length-1; i++)
            {
                //Console.WriteLine(pitchArr[i]);
                ch.Series["Series1"].Points.AddY(arr[i]);
                ch.Series["Series2"].Points.AddY(input);
            }
        }

        private void btnRollStart_Click(object sender, EventArgs e)
        {

        }
    }
}
