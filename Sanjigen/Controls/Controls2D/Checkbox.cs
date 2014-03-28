using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.Controls.Controls2D
{
    public class Checkbox : Control2D
    {
        private bool mvarChecked = false;
        public bool Checked { get { return mvarChecked; } set { mvarChecked = value; } }

        protected internal override void OnMouseUp(Input.Mouse.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mvarChecked = !mvarChecked;
        }

        protected internal override void OnRender(RenderEventArgs e)
        {
            e.Canvas.Color = UniversalEditor.Colors.Black;
            if (mvarChecked)
            {
                e.Canvas.DrawText("[X] " + base.Text, base.Position.X, base.Position.Y);
            }
            else
            {
                e.Canvas.DrawText("[ ] " + base.Text, base.Position.X, base.Position.Y);
            }
        }
    }
}
