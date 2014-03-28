using Caltron.Input.Mouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;

namespace Caltron
{
    public abstract class ChildWindow : Control2D, IControlContainer
    {
        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private ControlCollection mvarControls = null;
        public ControlCollection Controls { get { return mvarControls; } }

        public void Refresh()
        {
            if (Parent != null) Parent.Refresh();
        }

        public ChildWindow()
        {
            mvarControls = new ControlCollection(this);
        }

        protected internal override void OnCreated(EventArgs e)
        {
            base.OnCreated(e);

            List<Control> ctls = new List<Control>();
            foreach (Control ctl in mvarControls)
            {
                ctls.Add(ctl);
            }
            foreach (Control ctl in ctls)
            {
                ctl.OnCreated(e);
            }
        }

        protected internal override void OnBeforeRender(RenderEventArgs e)
        {
            base.OnBeforeRender(e);

            // Internal.OpenGL.Methods.glMatrixMode(MatrixMode.ModelView);
            // Internal.OpenGL.Methods.glLoadIdentity();

            e.Canvas.Translate(0, System.Windows.Forms.SystemInformation.CaptionHeight - 4);

            Internal.OpenGL.Methods.glDisable(Internal.OpenGL.Constants.GLCapabilities.Lighting);

            if (HasFocus)
            {
                e.Canvas.Color = Colors.DarkBlue;
            }
            else
            {
                e.Canvas.Color = Colors.Silver;
            }
            e.Canvas.DrawRectangle(0, 0, this.Size.Width, this.Size.Height);
            e.Canvas.FillRectangle(0, 0, this.Size.Width, 20);

            e.Canvas.Color = Color.FromRGBA(Colors.Silver.Red - 0.01, Colors.Silver.Green - 0.01, Colors.Silver.Blue - 0.01);
            e.Canvas.FillRectangle(0, 20, this.Size.Width - 1, this.Size.Height - 20);

            if (HasFocus)
            {
                e.Canvas.Color = Colors.White;
            }
            else
            {
                e.Canvas.Color = Colors.DarkGray;
            }
            e.Canvas.DrawText(mvarTitle, 4, 14);


            e.Canvas.Translate(0, System.Windows.Forms.SystemInformation.CaptionHeight - 4);
        }

        protected internal override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

            List<Controls.Controls2D.Popup> popups = new List<Controls.Controls2D.Popup>();
            foreach (Control ctl in mvarControls)
            {
                if (!ctl.Visible) continue;
                if (ctl is Controls.Controls2D.Popup)
                {
                    popups.Add(ctl as Controls.Controls2D.Popup);
                    continue;
                }

                RenderControl(e, ctl);
            }
            foreach (Controls.Controls2D.Popup popup in popups)
            {
                RenderControl(e, popup);
            }
        }

        private void RenderControl(RenderEventArgs e, Control ctl)
        {
            if (ctl is Control2D)
            {
                Control2D c2d = (ctl as Control2D);
                Internal.OpenGL.Methods.glMatrixMode(MatrixMode.ModelView);
                Internal.OpenGL.Methods.glTranslated(c2d.Position.X, c2d.Position.Y, 0);
                Internal.OpenGL.Methods.glTranslated(5, 1, 0);
            }

            ctl.OnBeforeRender(e);
            ctl.OnRender(e);
            ctl.OnAfterRender(e);

            if (ctl is Control2D)
            {
                Control2D c2d = (ctl as Control2D);
                Internal.OpenGL.Methods.glMatrixMode(MatrixMode.ModelView);
                Internal.OpenGL.Methods.glTranslated(-c2d.Position.X, -c2d.Position.Y, 0);
                Internal.OpenGL.Methods.glTranslated(-5, -1, 0);
            }
        }

        private bool mvarMoving = false;
        private double cx = 0.0, cy = 0.0;
        private int dx = 0, dy = 0;

        protected internal override void OnMouseDown(Input.Mouse.MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Control2D ctl = HitTest(e.X, e.Y);
            if (ctl != null)
            {
                MouseEventArgs args = new MouseEventArgs(e.Buttons, (int)(e.X - ctl.Position.X), (int)(e.Y - ctl.Position.Y));
                ctl.OnMouseDown(args);
                pressedctl = ctl;
                return;
            }

            if (e.Buttons == Input.Mouse.MouseButton.Primary && e.Y <= 20)
            {
                mvarMoving = true;
                cx = base.Position.X;
                cy = base.Position.Y;
                dx = e.X;
                dy = e.Y;
            }
        }
        protected internal override void OnMouseMove(Input.Mouse.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mvarMoving)
            {
                base.Position = new PositionVector2((e.X - dx), (e.Y - dy));
                base.Parent.Refresh();
            }
            else
            {
                Control2D ctl = HitTest(e.X, e.Y);
                if (ctl != null)
                {
                    MouseEventArgs args = new MouseEventArgs(e.Buttons, (int)(e.X - ctl.Position.X + mvarOffsetX), (int)(e.Y - ctl.Position.Y + mvarOffsetY));
                    // args = new MouseEventArgs(e.Buttons, (int)(e.X), (int)(e.Y));
                    ctl.OnMouseMove(args);
                }
            }
        }

        private int mvarOffsetX = -6;
        private int mvarOffsetY = -20;

        private Control2D HitTest(int x, int y)
        {
            x += mvarOffsetX;
            y += mvarOffsetY;
            // x += (int)this.Position.X;
            // y += 42;
            // y -= 20;
            /*
            if (Parent is Window && (Parent as Window).FullScreen)
            {
                y -= System.Windows.Forms.SystemInformation.CaptionHeight;
                y -= 5;
            }
            */
            foreach (Control ctl in this.Controls)
            {
                if (!ctl.Visible) continue;

                if (ctl is Control2D)
                {
                    Control2D c2d = (ctl as Control2D);
                    if (x >= c2d.Position.X && y >= c2d.Position.Y && x <= c2d.Position.X + c2d.Size.Width && y <= c2d.Position.Y + c2d.Size.Height)
                    {
                        return c2d;
                    }
                }
            }
            return null;
        }
        private Control pressedctl = null;

        protected internal override void OnMouseUp(Input.Mouse.MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (pressedctl != null)
            {
                Control2D c2d = (pressedctl as Control2D);
                if (c2d == null) return;

                MouseEventArgs args = new MouseEventArgs(e.Buttons, (int)(e.X - c2d.Position.X), (int)(e.Y - c2d.Position.Y));
                pressedctl.OnMouseUp(args);
                pressedctl = null;
                return;
            }

            Control2D ctl = HitTest(e.X, e.Y);
            if (ctl != null)
            {
                MouseEventArgs args = new MouseEventArgs(e.Buttons, (int)(e.X - ctl.Position.X), (int)(e.Y - ctl.Position.Y));
                ctl.OnMouseUp(args);
                return;
            }

            if (e.Buttons == Input.Mouse.MouseButton.Primary)
            {
                mvarMoving = false;
            }
        }
    }
}
