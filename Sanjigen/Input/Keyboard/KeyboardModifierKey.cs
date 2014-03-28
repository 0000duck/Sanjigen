using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.Input.Keyboard
{
    [Flags()]
    public enum KeyboardModifierKey
    {
        None = 0,
        Shift = 1,
        Control = 2,
        Alt = 4,
        Meta = 8
    }
}
