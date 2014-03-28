using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.Controls.Controls2D
{
    public abstract class Popup : Control2D, IControlContainer
    {
        private ControlCollection mvarControls = null;
        public ControlCollection Controls { get { return mvarControls; } }

        public void Refresh()
        {
            if (Parent != null) Parent.Refresh();
        }

        public Popup()
        {
            mvarControls = new ControlCollection(this);
            Visible = false;
        }
    }
}
