using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;

namespace Caltron.Controls.Controls2D
{
	public class Screen : Control2D, IControlContainer
	{
		public Screen()
		{
			mvarControls = new Control.ControlCollection(this);
		}

		public Window GetParentWindow()
		{
			IControlContainer parent = Parent;
			while (parent != null)
			{
				if (parent is Window) return (parent as Window);
				parent = parent.Parent;
			}
			return null;
		}

		private Control.ControlCollection mvarControls = null;
		public Control.ControlCollection Controls { get { return mvarControls; } }

		private Color mvarBackgroundColor = Colors.White;
		public Color BackgroundColor { get { return mvarBackgroundColor; } set { mvarBackgroundColor = value; } }

		protected internal override void OnCreated(EventArgs e)
		{
			base.OnCreated(e);
			Size = GetParentWindow().Size;
		}

		protected internal override void OnRender(RenderEventArgs e)
		{
			base.OnRender(e);

			e.Canvas.Clear(mvarBackgroundColor);
			foreach (Control ctl in mvarControls)
			{
				BeforeRenderEventArgs bre = new BeforeRenderEventArgs(e.Canvas);
				ctl.OnBeforeRender(bre);

				RenderEventArgs re = new RenderEventArgs(e.Canvas);
				ctl.OnRender(re);
				ctl.OnAfterRender(re);
			}
		}

		public void Refresh()
		{
			if (Parent != null) Parent.Refresh();
		}
	}
}
