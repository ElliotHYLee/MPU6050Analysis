using SlimDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MPU6050DataCollector.Controllers;

namespace MPU6050DataCollector
{
    public partial class JoyStickController : Form
    {
        DirectInput Input = new DirectInput();
        SlimDX.DirectInput.Joystick stick;
        Joystick[] Sticks;
        Joystick stick1;

        bool isOn;

        //Thumstick variables.
        int yValue = 0;
        int xValue = 0;
        int zValue = 0;
        int rotationZValue = 0;
        int rotationXValue = 0;
        int rotationYValue = 0;
        bool isFirst = true;
        int oldPwm = 0;
        int newPwm = 0;

        double oldPitch = 0;
        double newPitch = 0;

        double oldRoll = 0;
        double newRoll = 0;

        double oldYaw;
        double newYaw;
        JoystickState state;

        internal MainController _mainCtrl;


        static private JoyStickController _instance=null;

        static public JoyStickController Instance
        {
            get
            {
                if (_instance==null) _instance = new JoyStickController();
                return _instance;
            }
        }

        private void close(object sender, FormClosingEventArgs e)
        {
            isOn = false;
            _instance = null;
            
        }


        internal JoyStickController()
        {
            InitializeComponent();
            isOn = true;
            this.FormClosing += close;
            GetSticks();
            Sticks = GetSticks();
            stick1 = Sticks[0];
            isFirst = true;
            
            //Console.WriteLine("here");
            state = new JoystickState();

            
            //MessageBox.Show(newYaw.ToString());

        }

        internal void setUp(MainController controller, string defaultHeading)
        {
            this._mainCtrl = controller;
            newYaw = double.Parse(defaultHeading) / 100;
        }



        private void JoyStickController_Load(object sender, EventArgs e)
        {
            Joystick[] joystick = GetSticks();
            this.timer1.Enabled = true;
            
        }

        void StickHandlingLogic(Joystick stick, int id)
        {
            //Console.WriteLine("here2");
            // Creates an object from the class JoystickState.
            

            state = stick.GetCurrentState(); //Gets the state of the joystick
            //Console.WriteLine(state);
            //These are for the thumbstick readings
            yValue = state.Y;
            xValue = state.X;
            zValue = state.Z;
            rotationZValue = state.RotationZ;
            rotationXValue = state.RotationY;
            rotationYValue = state.RotationX;

            int th = 0;
            int[] z = state.GetSliders();
            th = z[0];
            if (z[0] == 0 && isFirst)
            {
                th = 0;

            }
            else
            {
                if (isFirst) isFirst = false;
                if (th >= 0)
                {
                    th = 50 - th / 2;
                }
                else
                {
                    th = -th / 2 + 50;
                }
            }


            //Console.Write("thrust = " + th);
            this.txtThrustPercent.Text = th.ToString();
            this.txtThrustPWM.Text = (1101 + (2000 - 1101) * th / 100).ToString();
            this.oldPwm = newPwm;
            this.newPwm = (1101 + (2000 - 1101) * th / 100);

            //Console.WriteLine(" x = " + xValue + " y = " + yValue + " z = " + zValue + " rot x = " + rotationXValue + " rot y = " + rotationYValue + " rot Z = " + rotationZValue);

            double pitchMaxAngle = 5; //3 degree
            double rollMaxAngle = 5;
            double yawMaxAgnle = 5; //90 degree

            oldPitch = newPitch;
            oldRoll = newRoll;
            oldYaw = newYaw;




            double pitch = (yValue* pitchMaxAngle / 100);
            double roll = (xValue * rollMaxAngle / 100);
            double yaw = (rotationZValue * yawMaxAgnle / 100);

            newPitch = pitch;
            newRoll = roll;
            newYaw = oldYaw + yaw;
            if (newYaw > 180) newYaw = -360 + newYaw;
            if (newYaw < -180) newYaw = 360 + newYaw;
            //send
            this._mainCtrl.updateRefAttPitch(pitch);
            this._mainCtrl.updateRefAttRoll(roll);
            this._mainCtrl.updateRefAttYaw(newYaw);

            // update window
            this.txtPitch.Text = pitch.ToString();
            this.txtRoll.Text = roll.ToString();
            this.txtYaw.Text = newYaw.ToString();



            bool[] buttons = state.GetButtons(); // Stores the number of each button on the gamepad into the bool[] butons.
                                                 // Console.WriteLine("# of button = " + buttons.Length);
                                                 //Here is an example on how to use this for the joystick in the first index of the array list
            if (id == 0)
            {
                // This is when button 0 of the gamepad is pressed, the label will change. Button 0 should be the square button.
                if (buttons[0])
                {

                }

            }
        }

        public Joystick[] GetSticks()
        {

            List<SlimDX.DirectInput.Joystick> sticks = new List<SlimDX.DirectInput.Joystick>(); // Creates the list of joysticks connected to the computer via USB.

            foreach (DeviceInstance device in Input.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                // Creates a joystick for each game device in USB Ports
                try
                {
                    stick = new SlimDX.DirectInput.Joystick(Input, device.InstanceGuid);
                    stick.Acquire();

                    // Gets the joysticks properties and sets the range for them.
                    foreach (DeviceObjectInstance deviceObject in stick.GetObjects())
                    {
                        if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-100, 100);
                    }

                    // Adds how ever many joysticks are connected to the computer into the sticks list.
                    sticks.Add(stick);
                }
                catch (DirectInputException)
                {
                }
            }
            Console.WriteLine(sticks.Count);
            return sticks.ToArray();
        }

        //Creates the StickHandlingLogic Method which takes all the joysticks in the sticks List and puts them into a timer.
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //Console.WriteLine("here333");
         //   for (int i = 0; i < Sticks.Length; i++)
         //   {
         //       StickHandlingLogic(Sticks[i], i);
         //   }

            if(isOn) StickHandlingLogic(stick1, 0);
            
            if (oldPwm != newPwm)
            {
                Console.WriteLine("oldPwm" + oldPwm);
                Console.WriteLine("newPwm" + newPwm);
                int pwm = Int32.Parse(this.txtThrustPWM.Text);
                this._mainCtrl.updateThrottle(pwm);
                
            }


            // send the msg to MCU
            

            /*
            if (oldPitch != newPitch)
            {
                this._mainCtrl.updateRefAttPitch(newPitch);
                if (newPitch == 0) 
                {
                    this._mainCtrl.updateRefAttPitch(newPitch);
                    this._mainCtrl.updateRefAttPitch(newPitch);
                    this._mainCtrl.updateRefAttPitch(newPitch);
                }
            }

            if (oldRoll != newRoll)
            {
                this._mainCtrl.updateRefAttRoll(newRoll);
                if (newRoll == 0)
                {
                    this._mainCtrl.updateRefAttRoll(newRoll);
                    this._mainCtrl.updateRefAttRoll(newRoll);
                    this._mainCtrl.updateRefAttRoll(newRoll);
                }
            }

            if (oldYaw != newYaw)
            {
                this._mainCtrl.updateRefAttYaw(newYaw);
                if (newYaw == 0)
                {
                    this._mainCtrl.updateRefAttYaw(newYaw);
                    this._mainCtrl.updateRefAttYaw(newYaw);
                    this._mainCtrl.updateRefAttYaw(newYaw);
                }

            }

           */ 


        }


    }
}
