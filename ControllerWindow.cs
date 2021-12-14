using System;
using System.Windows.Forms;

namespace ControllerTest
{
    public partial class ControllerWindow : Form
    {
        bool Button0 = false;
        public ControllerWindow()
        {
            InitializeComponent();
        }

        private void controller1_Update(object sender, EventArgs e)
        {
            if (Button0 != controller1.Buttons[0])
            {
                Button0 = controller1.Buttons[0];
                if (Button0)
                {
                    //Button is pressed
                }
                else
                {
                    //Button is not pressed
                }
            }
        }
    }
}
