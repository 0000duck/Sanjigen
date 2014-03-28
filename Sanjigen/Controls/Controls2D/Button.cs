using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;

namespace Caltron.Controls.Controls2D
{
    public class Button : Control2D
    {
        public event EventHandler Click;
        protected virtual void OnClick(EventArgs e)
        {
            if (Click != null) Click(this, e);
        }

        private Color mvarForegroundColor = Colors.Black;
        public Color ForegroundColor { get { return mvarForegroundColor; } set { mvarForegroundColor = value; } }

        private bool mvarPressed = false;

        protected internal override void OnRender(RenderEventArgs e)
        {
            e.Canvas.Color = Colors.Silver; // mvarBackgroundColor;
            e.Canvas.FillRectangle(0, 0, Size.Width, Size.Height);
            if (mvarPressed)
            {
                e.Canvas.DrawSunkenBorder(0, 0, Size.Width, Size.Height, 2);
            }
            else
            {
                e.Canvas.DrawRaisedBorder(0, 0, Size.Width, Size.Height, 2);
            }

            e.Canvas.Color = mvarForegroundColor;
            double x = 0, y = 0;
            Dimension2D dim = e.Canvas.MeasureText(base.Text);
            x = ((Size.Width - dim.Width) / 2);
            y = ((Size.Height - dim.Height) / 2) + 12;
            e.Canvas.DrawText(Text, x, y);
        }

        protected internal override void OnMouseDown(Input.Mouse.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Buttons == Input.Mouse.MouseButton.Primary)
            {
                mvarPressed = true;
            }
        }
        protected internal override void OnMouseUp(Input.Mouse.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Buttons == Input.Mouse.MouseButton.Primary)
            {
                mvarPressed = false;
                OnClick(e);
            }
            else
            {
                mvarPressed = false;
            }
        }
    }
}
