using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace Caltron.Controls.Controls2D.Screens
{
	public class MenuItem
	{
		public class MenuItemCollection
			: System.Collections.ObjectModel.Collection<MenuItem>
		{
			public MenuItem Add(string name, string text = null, PictureObjectModel image = null, bool visible = true, bool enabled = true)
			{
				if (text == null) text = name;
				MenuItem item = new MenuItem();
				item.Name = name;
				item.Text = text;
				item.Image = image;
				item.Visible = visible;
				item.Enabled = enabled;
				Add(item);
				return item;
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		private PictureObjectModel mvarImage = null;
		public PictureObjectModel Image { get { return mvarImage; } set { mvarImage = value; } }

		private bool mvarVisible = true;
		public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }

		private bool mvarEnabled = true;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }
	}
	
	public delegate void MenuItemActivatedEventHandler(object sender, MenuItemActivatedEventArgs e);
	public class MenuItemActivatedEventArgs : EventArgs
	{
		private MenuItem mvarMenuItem = null;
		public MenuItem MenuItem { get { return mvarMenuItem; } }

		public MenuItemActivatedEventArgs(MenuItem item)
		{
			mvarMenuItem = item;
		}
	}

	public class MenuScreen : Screen
	{
		private MenuItem.MenuItemCollection mvarMenuItems = new MenuItem.MenuItemCollection();
		public MenuItem.MenuItemCollection MenuItems { get { return mvarMenuItems; } }

		public event MenuItemActivatedEventHandler MenuItemActivated;
		protected virtual void OnMenuItemActivated(MenuItemActivatedEventArgs e)
		{
			if (MenuItemActivated != null) MenuItemActivated(this, e);
		}

		private int mvarSelectedMenuItemIndex = 0;

		public MenuItem SelectedMenuItem
		{
			get { return mvarMenuItems[mvarSelectedMenuItemIndex]; }
			set { mvarMenuItems[mvarSelectedMenuItemIndex] = value; }
		}

		protected internal override void OnKeyDown(Input.Keyboard.KeyboardEventArgs e)
		{
			base.OnKeyDown(e);
			switch (e.Keys)
			{
				case Input.Keyboard.KeyboardKey.ArrowDown:
				{
					if (mvarSelectedMenuItemIndex + 1 < mvarMenuItems.Count)
					{
						mvarSelectedMenuItemIndex++;
					}
					else
					{
						mvarSelectedMenuItemIndex = 0;
					}
					Refresh();
					break;
				}
				case Input.Keyboard.KeyboardKey.ArrowUp:
				{
					if (mvarSelectedMenuItemIndex - 1 >= 0)
					{
						mvarSelectedMenuItemIndex--;
					}
					else
					{
						mvarSelectedMenuItemIndex = mvarMenuItems.Count - 1;
					}
					Refresh();
					break;
				}
				case Input.Keyboard.KeyboardKey.Enter:
				{
					OnMenuItemActivated(new MenuItemActivatedEventArgs(mvarMenuItems[mvarSelectedMenuItemIndex]));
					break;
				}
			}
		}

		protected internal override void OnRender(RenderEventArgs e)
		{
			base.OnRender(e);

			double y = 32;
			foreach (MenuItem mi in mvarMenuItems)
			{
				e.Canvas.Color = Colors.Black;
				if (mi == SelectedMenuItem)
				{
					e.Canvas.Color = Colors.Red;
				}
				e.Canvas.DrawText(mi.Text, 32, y);
				y += 32;
			}
		}
	}
}
