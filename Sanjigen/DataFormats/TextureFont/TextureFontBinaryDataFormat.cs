using System;
using UniversalEditor;
using UniversalEditor.IO;
using Caltron.ObjectModels.TextureFont;

namespace Caltron.DataFormats.TextureFont
{
	public class TextureFontBinaryDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal ();
				_dfr.Capabilities.Add (typeof(TextureFontObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		
		private float mvarFormatVersion = 1.0f;
		public float FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }
		
		#region implemented abstract members of UniversalEditor.DataFormat
		protected override void LoadInternal (ref ObjectModel objectModel)
		{
			TextureFontObjectModel font = (objectModel as TextureFontObjectModel);
			if (font == null) throw new ObjectModelNotSupportedException();
			
			Reader reader = base.Accessor.Reader;
			base.Accessor.DefaultEncoding = Encoding.UTF8;
			
			string signature = reader.ReadFixedLengthString (8);
			if (signature != "GLTEXFNT") throw new InvalidDataFormatException("File does not begin with 'GLTEXFNT'");
			
			mvarFormatVersion = reader.ReadSingle ();
			
			font.GlyphWidth = reader.ReadInt32 ();
			font.GlyphHeight = reader.ReadInt32 ();
			font.TextureFileName = reader.ReadNullTerminatedString ();
			
			int charCount = reader.ReadInt32 ();
			
			char[] chars = new char[charCount];
			int[] poses = new int[charCount];
			
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = reader.ReadChar ();
			}
			for (int i = 0; i < charCount; i++)
			{
				int index = reader.ReadInt32 ();
				poses[i] = index;
			}
			for (int i = 0; i < charCount; i++)
			{
				font.Characters.Add(new TextureFontCharacter(chars[i], poses[i]));
			}
		}

		protected override void SaveInternal (ObjectModel objectModel)
		{
			TextureFontObjectModel font = (objectModel as TextureFontObjectModel);
			if (font == null) throw new ObjectModelNotSupportedException();
			
			Writer writer = base.Accessor.Writer;
			base.Accessor.DefaultEncoding = Encoding.UTF8;
			
			writer.WriteFixedLengthString("GLTEXFNT");
			writer.WriteSingle (mvarFormatVersion);
			writer.WriteInt32 (font.GlyphWidth);
			writer.WriteInt32 (font.GlyphHeight);
			writer.WriteNullTerminatedString (font.TextureFileName);
			
			writer.WriteInt32 (font.Characters.Count);
			foreach (TextureFontCharacter charpos in font.Characters)
			{
				writer.WriteChar (charpos.Character);
			}
			foreach (TextureFontCharacter charpos in font.Characters)
			{
				writer.WriteInt32 ((int)charpos.Index);
			}
			
			writer.Flush ();
		}
		#endregion
		
	}
}

