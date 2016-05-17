/*============================================================
// Made by: Elliot Hongyun Lee
// Undergrad Research Project
============================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPU6050DataCollector.Model
{
    class AttitudeData
    {
        private List<string>[] _gyro;
        private List<string>[] _acc;
        private List<string>[] _cFilter;
        private List<string>[] _motor;
        private List<string>[] _mag;
        private List<string>[] _ctrlRefAtt;
        private List<string> _throttle;

        public AttitudeData()
        {
            this._gyro = new List<string>[10];
            this._acc = new List<string>[10];
            this._cFilter = new List<string>[10];
            this._motor = new List<string>[6];
            this._mag = new List<string>[3];
            this._ctrlRefAtt = new List<string>[3];
            this._throttle = new List<string>();


            for (int i = 0; i < 10; i++)
            {
                //if (i > 6) i = 10; // finish the loop early.
                this._gyro[i] = new List<string>();
                this._acc[i] = new List<string>();
                this._cFilter[i] = new List<string>();
                if (i < 6)
                {
                    this._motor[i] = new List<string>();
                }
                if (i < 3)
                {
                    this._mag[i] = new List<string>();
                    this._ctrlRefAtt[i] = new List<string>();
                }
                
                
            }

            
        }

        public List<string>[] mag
        {
            set { this._mag = value; }
            get { return this._mag; }
        }

        public List<string>[] motor
        {
            set { this._motor = value; }
            get { return this._motor; }
        }

        public List<string>[] gyro
        {
            set { this._gyro=value;}
            get { return this._gyro; }
        }

        public List<string>[] acc
        {
            set { this._acc = value;}
            get { return this._acc; }
        }

        public List<string>[] cFilter
        {
            set { this._cFilter=value;}
            get { return this._cFilter; }
        }

        public List<string>[] ctrlRefAtt
        {
            set { this._ctrlRefAtt = value; }
            get { return this._ctrlRefAtt; }
        }

        public List<string> Throttle
        {
            get { return _throttle; }
            set { _throttle = value; }
        }


    }
}
