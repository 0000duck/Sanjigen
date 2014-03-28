using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.Input.Joystick
{
    public class JoystickEventArgs : EventArgs
    {
        private JoystickButton mvarButtons = JoystickButton.None;
        public JoystickButton Buttons
        {
            get { return mvarButtons; }
        }

        private double mvarX1 = 0.0;
        public double X1
        {
            get { return mvarX1; }
        }
        private double mvarX2 = 0.0;
        public double X2
        {
            get { return mvarX2; }
        }
        private double mvarY1 = 0.0;
        public double Y1
        {
            get { return mvarY1; }
        }
        private double mvarY2 = 0.0;
        public double Y2
        {
            get { return mvarY2; }
        }

        public JoystickEventArgs(JoystickButton buttons, double x1, double y1, double x2, double y2)
        {
            mvarButtons = buttons;
        }
    }
}
