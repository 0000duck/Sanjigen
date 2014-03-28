using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace Caltron.Controls.Controls2D
{
    public class ScrollBar : Control2D
    {
        private ScrollBarOrientation mvarOrientation = ScrollBarOrientation.Horizontal;
        public ScrollBarOrientation Orientation { get { return mvarOrientation; } set { mvarOrientation = value; } }

        private double mvarMinimum = 0.0;
        public double Minimum { get { return mvarMinimum; } set { mvarMinimum = value; } }

        private double mvarMaximum = 100.0;
        public double Maximum { get { return mvarMaximum; } set { mvarMaximum = value; } }

        private double mvarValue = 0.0;
        public double Value { get { return mvarValue; } set { mvarValue = value; } }

        private Color mvarBackgroundColor = Colors.DarkGray;
        public Color BackgroundColor { get { return mvarBackgroundColor; } set { mvarBackgroundColor = value; } }

        protected internal override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

            e.Canvas.FillRectangle(0, 0, Size.Width, Size.Height, mvarBackgroundColor);

            #region Left Arrow
            e.Canvas.FillRectangle(0, 0, 8, Size.Height - 1, Colors.LightGray);
            e.Canvas.Color = Colors.Black;
            e.Canvas.DrawRectangle(0, 0, 8, Size.Height - 1);

            e.Canvas.DrawLine(8 - 1, 4, 0, Size.Height / 2);
            e.Canvas.DrawLine(8, Size.Height - 4, 0, Size.Height / 2);
            #endregion
            #region Thumb
            double thw = 9;
            double thx = (this.Size.Width * ((this.Value - this.Minimum) / (this.Maximum - this.Minimum))) - (thw * 2), thy = 0;
            e.Canvas.FillRectangle(9 + thx, thy, Size.Height - 1, Size.Height - 1, Colors.LightGray);
            e.Canvas.Color = Colors.Black;
            e.Canvas.DrawRectangle(9 + thx, thy, Size.Height - 1, Size.Height - 1);

            e.Canvas.Translate(0, 14);
            e.Canvas.DrawText("#", 9 + thx + 5, thy);
            e.Canvas.Translate(0, -14);
            #endregion
            #region Right Arrow
            e.Canvas.FillRectangle(Size.Width - 8, 0, 8, Size.Height - 1, Colors.LightGray);
            e.Canvas.Color = Colors.Black;
            e.Canvas.DrawRectangle(Size.Width - 8, 0, 8, Size.Height - 1);

            e.Canvas.DrawLine(Size.Width - 8 - 1, 4, Size.Width - 1, Size.Height / 2);
            e.Canvas.DrawLine(Size.Width - 8, Size.Height - 4, Size.Width - 1, Size.Height / 2);
            #endregion
        }
    }
    public enum ScrollBarOrientation
    {
        Horizontal,
        Vertical
    }
}
