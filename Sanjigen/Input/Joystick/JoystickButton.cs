using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.Input.Joystick
{
    public enum JoystickButton
    {
        None = 0,
        Button1 = 1,
        Button2 = 2,
        Button3 = 3,
        Button4 = 4,
        Button5 = 5,
        Button6 = 6,
        Button7 = 7,
        Button8 = 8,

        #region PlayStation 2 Gamepad Specific
        PlaystationSquare = Button1,
        PlaystationTriangle,
        PlaystationCircle,
        PlaystationX,
        PlaystationTriggerTopLeft = Button5,
        PlaystationTriggerBottomLeft = Button6,
        PlaystationTriggerTopRight = Button7,
        PlaystationTriggerBottomRight = Button8
        #endregion
        #region Joystick Specific
        #endregion
    }
}
