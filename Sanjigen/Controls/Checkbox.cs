using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.Controls
{
    public class Checkbox : Control2D
    {
        private bool mvarChecked = false;
        public bool Checked { get { return mvarChecked; } set { mvarChecked = value; } }

        private string mvarText = String.Empty;
        public string Text { get { return mvarText; } set { mvarText = value; } }

        protected internal override void OnMouseUp(Input.Mouse.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mvarChecked = !mvarChecked;
        }

        protected internal override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);
            e.Canvas.Color = UniversalEditor.Colors.Black;
            if (mvarChecked)
            {
                e.Canvas.DrawText("[X] " + mvarText, base.Position.X, base.Position.Y);
            }
            else
            {
                e.Canvas.DrawText("[ ] " + mvarText, base.Position.X, base.Position.Y);
            }
        }
    }
}
