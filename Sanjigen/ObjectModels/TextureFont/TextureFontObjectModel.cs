using System;
using UniversalEditor;


namespace Caltron.ObjectModels.TextureFont
{
	public class TextureFontObjectModel : ObjectModel
	{
		#region implemented abstract members of UniversalEditor.ObjectModel
		public override void Clear ()
		{
			mvarTextureFileName = String.Empty;
			mvarGlyphWidth = 0;
			mvarGlyphHeight = 0;
		}

		public override void CopyTo (ObjectModel where)
		{
			TextureFontObjectModel clone = (where as TextureFontObjectModel);
			clone.GlyphHeight = mvarGlyphHeight;
			clone.GlyphWidth = mvarGlyphWidth;
			clone.TextureFileName = (mvarTextureFileName.Clone () as string);
		}
		#endregion
		
		private string mvarTextureFileName = String.Empty;
		public string TextureFileName { get { return mvarTextureFileName; } set { mvarTextureFileName = value; } }
		
		private int mvarGlyphWidth = 0;
		public int GlyphWidth { get { return mvarGlyphWidth; } set { mvarGlyphWidth = value; } }
		
		private int mvarGlyphHeight = 0;
		public int GlyphHeight { get { return mvarGlyphHeight; } set { mvarGlyphHeight = value; } }
		
		private TextureFontCharacter.TextureFontCharacterCollection mvarCharacters = new TextureFontCharacter.TextureFontCharacterCollection();
		public TextureFontCharacter.TextureFontCharacterCollection Characters { get { return mvarCharacters; } }
		
	}
}

