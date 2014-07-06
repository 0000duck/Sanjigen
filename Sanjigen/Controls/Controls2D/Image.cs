using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace Caltron.Controls.Controls2D
{
	public enum ImageScaleMode
	{
		None,
		Center,
		Stretch
	}
	public class Image : Control2D
	{
		private ImageScaleMode mvarImageScaleMode = ImageScaleMode.None;
		public ImageScaleMode ImageScaleMode { get { return mvarImageScaleMode; } set { mvarImageScaleMode = value; } }

		private PictureObjectModel mvarPicture = null;
		public PictureObjectModel Picture { get { return mvarPicture; } set { mvarPicture = value; } }

		public Image(PictureObjectModel picture)
		{
			mvarPicture = picture;
		}

		protected internal override void OnRender(RenderEventArgs e)
		{
			base.OnRender(e);
			switch (mvarImageScaleMode)
			{
				case Controls2D.ImageScaleMode.Center:
				{
					e.Canvas.DrawImage((base.Size.Width - mvarPicture.Width) / 2, (base.Size.Height - mvarPicture.Height) / 2, mvarPicture.Width, mvarPicture.Height, mvarPicture);
					break;
				}
				case Controls2D.ImageScaleMode.None:
				{
					e.Canvas.DrawImage(0, 0, mvarPicture.Width, mvarPicture.Height, mvarPicture);
					break;
				}
				case Controls2D.ImageScaleMode.Stretch:
				{
					e.Canvas.DrawImage(0, 0, base.Size.Width, base.Size.Height, mvarPicture);
					break;
				}
			}
		}
	}
}
