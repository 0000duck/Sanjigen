using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;

namespace Caltron.Controls.Controls2D
{
    public class ComboBox : Control2D
    {
        private ComboBoxItem.ComboBoxItemCollection mvarItems = new ComboBoxItem.ComboBoxItemCollection();
        public ComboBoxItem.ComboBoxItemCollection Items { get { return mvarItems; } set { mvarItems = value; } }

        private ComboBoxItem mvarSelectedItem = null;
        public ComboBoxItem SelectedItem { get { return mvarSelectedItem; } set { mvarSelectedItem = value; } }

        private bool mvarIsDropDownOpen = false;
        public bool IsDropDownOpen { get { return mvarPopup.Visible; } }

        private class ComboBoxPopup : Popup
        {
            private ComboBox mvarParent = null;
            public ComboBoxPopup(ComboBox parent)
            {
                mvarParent = parent;
            }

            private ComboBoxItem highlightItem = null;

            protected internal override void OnMouseMove(Input.Mouse.MouseEventArgs e)
            {
                base.OnMouseMove(e);
                double y = 0.0;
                foreach (ComboBoxItem item in mvarParent.Items)
                {
                    if (e.Y >= y && e.Y <= (y + 18))
                    {
                        highlightItem = item;
                        Refresh();
                        break;
                    }
                    y += 18;
                }
            }
            protected internal override void OnRender(RenderEventArgs e)
            {
                base.OnRender(e);
                e.Canvas.FillRectangle(0, 0, Size.Width, Size.Height, Colors.White);
                e.Canvas.Color = Colors.Black;
                e.Canvas.DrawRectangle(0, 0, Size.Width, Size.Height);
                double y = 18.0;
                foreach (ComboBoxItem item in mvarParent.Items)
                {
                    if (highlightItem == item)
                    {
                        e.Canvas.Color = Colors.DarkBlue;
                        e.Canvas.FillRectangle(0, y - 18, Size.Width, 18);
                        e.Canvas.Color = Colors.White;
                    }
                    e.Canvas.DrawText(item.Text, 4, y - 4);
                    if (highlightItem == item)
                    {
                        e.Canvas.Color = Colors.Black;
                    }
                    y += 18;
                }
            }

            protected internal override void OnMouseDown(Input.Mouse.MouseEventArgs e)
            {
                base.OnMouseDown(e);
                if (e.Buttons == Input.Mouse.MouseButton.Primary)
                {
                    Visible = false;

                    double y = 16.0;
                    foreach (ComboBoxItem item in mvarParent.Items)
                    {
                        if (e.Y >= y && e.Y <= (y + 18))
                        {
                            mvarParent.SelectedItem = item;
                            break;
                        }
                        y += 18;
                    }
                }
            }
        }

        private ComboBoxPopup mvarPopup = null;
        public ComboBox()
        {
            mvarPopup = new ComboBoxPopup(this);
        }

        protected internal override void OnCreated(EventArgs e)
        {
            base.OnCreated(e);
            Parent.Controls.Add(mvarPopup);
        }

        protected internal override void OnMouseDown(Input.Mouse.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Buttons == Input.Mouse.MouseButton.Primary)
            {
                mvarPopup.Position = new PositionVector2(Position.X, Position.Y + Size.Height);

                double height = 18 * mvarItems.Count;
                // if (height > (18 * 5)) height = 18 * 5;
                mvarPopup.Size = new Dimension2D(Size.Width, height);

                mvarPopup.Visible = !mvarPopup.Visible;
                /*
                if (e.X <= Size.Width && e.X >= Size.Width - 9)
                {
                    mvarIsDropDownOpen = !mvarIsDropDownOpen;
                }
                */
            }
        }
        protected internal override void OnRender(RenderEventArgs e)
        {
            e.Canvas.Color = Colors.White;
            e.Canvas.FillRectangle(0, 0, Size.Width, Size.Height);

            if (IsDropDownOpen)
            {
                e.Canvas.Color = Colors.DarkGray;
            }
            else
            {
                e.Canvas.Color = Colors.LightGray;
            }
            e.Canvas.FillRectangle(Size.Width - 9, 2, 7, Size.Height - 4);
            e.Canvas.Color = Colors.Black;
            e.Canvas.DrawRectangle(Size.Width - 9, 2, 7, Size.Height - 4);

            e.Canvas.DrawLine(Size.Width - 9, 5, Size.Width - 6, Size.Height - 4);
            e.Canvas.DrawLine(Size.Width - 3, 5, Size.Width - 6, Size.Height - 4);

            e.Canvas.DrawSunkenBorder(0, 0, Size.Width, Size.Height, 2);

            if (mvarSelectedItem != null)
            {
                e.Canvas.Color = Colors.Black;

                Internal.OpenGL.Methods.glTranslated(0, 10, 0);
                e.Canvas.DrawText(mvarSelectedItem.Text, 4, 4);
                Internal.OpenGL.Methods.glTranslated(0, -10, 0);
            }
            /*
            if (mvarIsDropDownOpen)
            {
                Internal.OpenGL.Methods.glTranslated(0, Size.Height, 0);

                e.Canvas.FillRectangle(0, 0, Size.Width, 128, Colors.White);

                Internal.OpenGL.Methods.glTranslated(0, -Size.Height, 0);
            }
            */
        }
    }
    public class ComboBoxItem
    {
        public class ComboBoxItemCollection
            : System.Collections.ObjectModel.Collection<ComboBoxItem>
        {
            public ComboBoxItem Add(string text)
            {
                return Add(text, text);
            }
            public ComboBoxItem Add(string text, object value)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = text;
                item.Value = value;
                Add(item);
                return item;
            }
        }

        private string mvarText = String.Empty;
        public string Text { get { return mvarText; } set { mvarText = value; } }

        private object mvarValue = null;
        public object Value { get { return mvarValue; } set { mvarValue = value; } }
    }
}
