using System;

using UniversalEditor;

namespace Caltron.ObjectModels.TextureFont
{
	public class TextureFontCharacter
	{
		public class TextureFontCharacterCollection
			: System.Collections.ObjectModel.Collection<TextureFontCharacter>
		{
			public TextureFontCharacter this[char character]
			{
				get
				{
					foreach (TextureFontCharacter chr in this)
					{
						if (chr.Character == character) return chr;
					}
					return null;
				}
			}
		}
		
		private char mvarCharacter = default(char);
		public char Character { get { return mvarCharacter; } set { mvarCharacter = value; } }
		
		private int mvarIndex = 0;
		public int Index { get { return mvarIndex; } set { mvarIndex = value; } }
		
		public TextureFontCharacter(char character, int index)
		{
			mvarCharacter = character;
			mvarIndex = index;
		}
	}
}

