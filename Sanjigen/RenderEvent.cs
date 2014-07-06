using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace Caltron
{
    public delegate void RenderEventHandler(object sender, RenderEventArgs e);
	public delegate void BeforeRenderEventHandler(object sender, BeforeRenderEventArgs e);
    public class RenderEventArgs : EventArgs
    {
        public RenderEventArgs(Canvas canvas)
        {
            mvarCanvas = canvas;
        }

        private Canvas mvarCanvas = null;
        public Canvas Canvas { get { return mvarCanvas; } }
	}
	public class BeforeRenderEventArgs : System.ComponentModel.CancelEventArgs
	{
        public BeforeRenderEventArgs(Canvas canvas)
        {
            mvarCanvas = canvas;
        }

        private Canvas mvarCanvas = null;
        public Canvas Canvas { get { return mvarCanvas; } }

		private bool mvarDrawBackgroundColor = true;
		public bool DrawBackgroundColor { get { return mvarDrawBackgroundColor; } set { mvarDrawBackgroundColor = value; } }
	}
}
