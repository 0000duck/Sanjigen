using System;
namespace Caltron.ObjectModels
{
	public class TextureFontObjectModel
	{
		private string mvarTextureFileName = String.Empty;
		public string TextureFileName { get { return mvarTextureFileName; } set { mvarTextureFileName = value; } }
		
		private int mvarGlyphWidth = 0;
		public int GlyphWidth { get { return mvarGlyphWidth; } set { mvarGlyphWidth = value; } }
		
		private int mvarGlyphHeight = 0;
		public int GlyphHeight { get { return mvarGlyphHeight; } set { mvarGlyphHeight = value; } }
		
	}
}

