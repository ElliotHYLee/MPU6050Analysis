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
        public AttitudeData()
        {
            this._gyro = new List<string>[10];
            this._acc = new List<string>[10];
            this._cFilter = new List<string>[10];
            this._motor = new List<string>[4];
            for (int i = 0; i < 10; i++)
            {
                this._gyro[i] = new List<string>();
                this._acc[i] = new List<string>();
                this._cFilter[i] = new List<string>();
                if (i < 4)
                {
                    this._motor[i] = new List<string>();
                }
            }

            
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


    }
}
