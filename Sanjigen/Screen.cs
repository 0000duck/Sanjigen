using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;

namespace Caltron
{
    public abstract class Screen : Control2D
    {
        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        protected internal override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

            e.Canvas.Color = Colors.Silver;
            e.Canvas.DrawSquare(0, 0, 400, 300);
        }
    }
}
