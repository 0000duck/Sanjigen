using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.Input.Keyboard
{
    public class KeyboardEventArgs : EventArgs
    {
        private KeyboardKey mvarKeys = KeyboardKey.None;
        public KeyboardKey Keys
        {
            get { return mvarKeys; }
        }
        private KeyboardModifierKey mvarModifierKeys = KeyboardModifierKey.None;
        public KeyboardModifierKey ModifierKeys
        {
            get { return mvarModifierKeys; }
        }

        public KeyboardEventArgs(KeyboardKey keys, KeyboardModifierKey modifierKeys)
        {
            mvarKeys = keys;
            mvarModifierKeys = modifierKeys;
        }
    }
}
