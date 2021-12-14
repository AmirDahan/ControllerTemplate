using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SlimDX.DirectInput;

namespace ControllerTest
{
    public partial class Controller : UserControl
    {
        //Controller
        DirectInput dInput = new DirectInput();
        JoystickState State;
        Joystick[] Joysticks;
        int cntStick = 0;
        Label[] buttons;
        Label[] dpad;
        Timer UpdateTimer = new Timer();
        public event EventHandler Update;
        public static int refreshrate = 10;
        public static bool symbols = false;

        //Values
        public int RSX;
        public int RSY;
        public int LSX;
        public int LSY;
        public int RT;
        public int LT;
        public bool[] Buttons;
        public int Dpad;

        //Customization
        [Category("Behavior")]
        public int RefreshRate
        {
            get { return refreshrate; }
            set { refreshrate = value; UpdateTimer.Interval = value; }
        }
        [Category("Appearance")]
        public bool Symbols
        {
            get { return symbols; }
            set { symbols = value; }
        }

        public Controller()
        {
            InitializeComponent();
            buttons = new Label[]{
                SquareButton, CrossButton, CircleButton, TriangleButton,
                LeftBumper, RightBumper, LeftTrigger, RightTrigger,
                ShareButton, OptionsButton, LeftStick,
                RightStick, PlaystatuinButton
            };
            dpad = new Label[] {
                DpadCenter, DpadUp, DpadUpRight,
                DpadRight, DpadDownRight, DpadDown,
                DpadDownLeft, DpadLeft, DpadUpLeft
            };

            Joysticks = GetSticks();
            cntStick = Joysticks.Length;
        }
        private void LoadEvent(object sender, EventArgs e)
        {
            if (!symbols)
            {
                foreach (Label l in this.Controls.OfType<Label>())
                {
                    l.Text = "";
                }
            }
            UpdateTimer.Interval = RefreshRate;
            UpdateTimer.Tick += tmr_Tick;
            UpdateTimer.Start();
            UpdateTimer.Enabled = this.Enabled;
        }

        private void Controller_EnabledChanged(object sender, EventArgs e) { UpdateTimer.Enabled = this.Enabled; }

        public Joystick[] GetSticks()
        {
            var sticks = new List<Joystick>();
            foreach (DeviceInstance device in dInput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                try
                {
                    var stick = new Joystick(dInput, device.InstanceGuid);
                    stick.Acquire();

                    foreach (DeviceObjectInstance deviceObject in stick.GetObjects())
                    {
                        if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-1000, 1000);
                    }

                    sticks.Add(stick);

                    Console.WriteLine(stick.Information.InstanceName);
                }
                catch (DirectInputException)
                {
                }
            }
            return sticks.ToArray();
        }
        void tmr_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Joysticks == null || cntStick == 0) return;

                for (int i = 0; i < cntStick; i++)
                {

                    if (Joysticks[i].Acquire().IsFailure)
                    {
                        return;
                    }

                    if (Joysticks[i].Poll().IsFailure)
                    {
                        return;
                    }

                    State = Joysticks[i].GetCurrentState();

                    Buttons = State.GetButtons();
                    for (int j = 0; j < Buttons.Length; j++)
                    {
                        if (j > 12) break;

                        buttons[j].BackColor = State.IsPressed(j) ? Theme.Accent : Theme.Inactive;
                    }

                    int[] viewCtrl = State.GetPointOfViewControllers();

                    for (int iView = 0; iView < 9; iView++)
                    {
                        int tmp = 0;
                        if (viewCtrl[0] != -1) tmp = viewCtrl[0] / 4500 + 1;
                        else tmp = 0;

                        if (tmp == iView) { Dpad = iView; dpad[iView].BackColor = Theme.Accent; }
                        else dpad[iView].BackColor = Theme.Inactive;
                    }

                    int[] axisMovement = new int[] { State.X, State.Y, State.Z, State.RotationZ, State.RotationX, State.RotationY };
                    LSX = (axisMovement[0] + 1000) / 20;
                    LSY = (-axisMovement[1] + 1000) / 20;
                    RSX = (axisMovement[2] + 1000) / 20;
                    RSY = (-axisMovement[3] + 1000) / 20;
                    LT = (axisMovement[4] + 1000) / 20;
                    RT = (axisMovement[5] + 1000) / 20;

                    //Debugging only

                    //Touchpad.Text = string.Format("{0},{1},{2},{3},{4},{5}", LSX, LSY, RSX, RSY, LT, RT);
                    /*Touchpad.Text = 
                        "0: " + Buttons[0] + Environment.NewLine +
                        "1: " + Buttons[1] + Environment.NewLine +
                        "2: " + Buttons[2] + Environment.NewLine +
                        "3: " + Buttons[3] + Environment.NewLine +
                        "4: " + Buttons[4] + Environment.NewLine +
                        "5: " + Buttons[5] + Environment.NewLine +
                        "6: " + Buttons[6] + Environment.NewLine +
                        "7: " + Buttons[7] + Environment.NewLine +
                        "8: " + Buttons[8] + Environment.NewLine +
                        "9: " + Buttons[9] + Environment.NewLine +
                        "10: " + Buttons[10] + Environment.NewLine +
                        "11: " + Buttons[11] + Environment.NewLine;*/

                    LeftStick.Left = (LSX) - 15;
                    LeftStick.Top = (100 - LSY) - 15;
                    RightStick.Left = (RSX) - 15;
                    RightStick.Top = (100 - RSY) - 15;
                    LeftTriggerBar.Height = LT;
                    RightTriggerBar.Height = RT;
                    if (Update != null) Update.Invoke(this, e);
                }
            }
            catch
            {
                
            }
        }
    }
}
