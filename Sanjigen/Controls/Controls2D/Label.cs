using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;

namespace Caltron.Controls.Controls2D
{
    public class Label : Control2D
    {
        private HorizontalAlignment mvarHorizontalAlignment = HorizontalAlignment.Left;
        public HorizontalAlignment HorizontalAlignment { get { return mvarHorizontalAlignment; } set { mvarHorizontalAlignment = value; } }

        private Color mvarForegroundColor = Colors.Black;
        public Color ForegroundColor { get { return mvarForegroundColor; } set { mvarForegroundColor = value; } }

        protected internal override void OnRender(RenderEventArgs e)
        {
            e.Canvas.Color = mvarForegroundColor;
            e.Canvas.Translate(0, 12);
            double x = 0, y = 0;

            switch (mvarHorizontalAlignment)
            {
                case Caltron.HorizontalAlignment.Center:
                {
                    Dimension2D dim = e.Canvas.MeasureText(base.Text);
                    x = ((Size.Width + dim.Width) / 2);
                    break;
                }
                case Caltron.HorizontalAlignment.Right:
                {
                    Dimension2D dim = e.Canvas.MeasureText(base.Text);
                    x = (Size.Width - dim.Width);
                    break;
                }
            }
            e.Canvas.DrawText(base.Text, x, y);
            e.Canvas.Translate(0, -12);
        }
    }
}
