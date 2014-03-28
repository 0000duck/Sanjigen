using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.Input.Mouse
{
    public class MouseEventArgs : EventArgs
    {
        private MouseButton mvarButtons = MouseButton.None;
        public MouseButton Buttons
        {
            get { return mvarButtons; }
        }

        private int mvarX = 0;
        public int X
        {
            get { return mvarX; }
        }
        private int mvarY = 0;
        public int Y
        {
            get { return mvarY; }
        }

        public MouseEventArgs(MouseButton buttons, int x, int y)
        {
            mvarButtons = buttons;
            mvarX = x;
            mvarY = y;
        }
    }
}
