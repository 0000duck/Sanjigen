using Caltron.Input.Mouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public class Control
    {
        public class ControlCollection
            : System.Collections.ObjectModel.Collection<Control>
        {
            private IControlContainer mvarParent = null;
            public ControlCollection(IControlContainer parent)
            {
                mvarParent = parent;
            }

            public Control this[string Name]
            {
                get
                {
                    foreach (Control ctl in this)
                    {
                        if (ctl.Name == Name) return ctl;
                    }
                    return null;
                }
            }

            protected override void InsertItem(int index, Control item)
            {
                base.InsertItem(index, item);
                item.mvarParent = mvarParent;
                item.OnCreated(EventArgs.Empty);
            }
            protected override void RemoveItem(int index)
            {
                this[index].mvarParent = null;
                base.RemoveItem(index);
            }
            protected override void ClearItems()
            {
                foreach (Control ctl in this)
                {
                    ctl.mvarParent = null;
                }
                base.ClearItems();
            }
        }

        private IControlContainer mvarParent = null;
        public IControlContainer Parent { get { return mvarParent; } }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string mvarText = String.Empty;
        public string Text { get { return mvarText; } set { mvarText = value; } }

        private bool mvarVisible = true;
        public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }

        private bool mvarHasFocus = false;
        /// <summary>
        /// True if this control has the focus, false otherwise.
        /// </summary>
        public bool HasFocus { get { return mvarHasFocus; } internal set { mvarHasFocus = value; } }

        public event EventHandler GotFocus;
        protected internal virtual void OnGotFocus(EventArgs e)
        {
            if (GotFocus != null) GotFocus(this, e);
        }
        public event EventHandler LostFocus;
        protected internal virtual void OnLostFocus(EventArgs e)
        {
            if (LostFocus != null) LostFocus(this, e);
        }

        public event EventHandler Created;
        protected internal virtual void OnCreated(EventArgs e)
        {
            if (Created != null) Created(this, e);
        }

        public event BeforeRenderEventHandler BeforeRender;
        protected internal virtual void OnBeforeRender(BeforeRenderEventArgs e)
        {
            if (BeforeRender != null) BeforeRender(this, e);
        }

        public event RenderEventHandler Render;
        protected internal virtual void OnRender(RenderEventArgs e)
        {
            if (Render != null) Render(this, e);
        }

        public event RenderEventHandler AfterRender;
        protected internal virtual void OnAfterRender(RenderEventArgs e)
        {
            if (AfterRender != null) AfterRender(this, e);
        }

        #region Mouse
        public event MouseEventHandler MouseDown;
        protected internal virtual void OnMouseDown(MouseEventArgs e)
        {
            if (MouseDown != null) MouseDown(this, e);
        }
        public event MouseEventHandler MouseMove;
        protected internal virtual void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null) MouseMove(this, e);
        }
        public event MouseEventHandler MouseUp;
        protected internal virtual void OnMouseUp(MouseEventArgs e)
        {
            if (MouseUp != null) MouseUp(this, e);
        }
        public event MouseEventHandler MouseWheel;
        protected internal virtual void OnMouseWheel(MouseEventArgs e)
        {
            if (MouseWheel != null) MouseWheel(this, e);
        }
        #endregion
    }
}
