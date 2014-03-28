using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;

namespace Caltron
{
    public class Control3D : Control
    {
        private PositionVector3 mvarPosition = new PositionVector3();
        public PositionVector3 Position { get { return mvarPosition; } set { mvarPosition = value; } }

        private Dimension3D mvarSize = new Dimension3D();
        public Dimension3D Size { get { return mvarSize; } set { mvarSize = value; } }
    }
}
