using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace Caltron
{
    public class Control2D : Control
    {
        private PositionVector2 mvarPosition = new PositionVector2();
        public PositionVector2 Position { get { return mvarPosition; } set { mvarPosition = value; } }

        private Dimension2D mvarSize = new Dimension2D();
        public Dimension2D Size { get { return mvarSize; } set { mvarSize = value; } }
    }
}
