using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace Caltron
{
    public delegate void RenderEventHandler(object sender, RenderEventArgs e);
    public class RenderEventArgs : System.ComponentModel.CancelEventArgs
    {
        public RenderEventArgs(Canvas canvas)
        {
            mvarCanvas = canvas;
        }

        private Canvas mvarCanvas = null;
        public Canvas Canvas { get { return mvarCanvas; } }
    }
}
