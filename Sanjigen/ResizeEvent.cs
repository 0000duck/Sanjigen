using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public delegate void ResizeEventHandler(object sender, ResizeEventArgs e);
    public class ResizeEventArgs : EventArgs
    {
        private Dimension2D mvarOldSize = new Dimension2D();
        public Dimension2D OldSize { get { return mvarOldSize; } }
        private Dimension2D mvarNewSize = new Dimension2D();
        public Dimension2D NewSize { get { return mvarNewSize; } }

        public ResizeEventArgs(Dimension2D oldSize, Dimension2D newSize)
        {
            mvarOldSize = oldSize;
            mvarNewSize = newSize;
        }
    }
}
