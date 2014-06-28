using System;
using System.Collections.Generic;

using UniversalEditor;
using UniversalEditor.Accessors;

using Caltron.ObjectModels.TextureFont;

namespace Caltron
{
	public abstract class Font
	{
	}
	public static class Fonts
	{
		private static Font mvarStrokeRoman = new SystemFont(SystemFontType.Stroke, new IntPtr(0));
		public static Font StrokeRoman { get { return mvarStrokeRoman; } }
		
		private static Font mvarStrokeRomanMonospaced = new SystemFont(SystemFontType.Stroke, new IntPtr(1));
		public static Font StrokeRomanMonospaced { get { return mvarStrokeRomanMonospaced; } }
		
		private static Font mvarBitmap9By15 = new SystemFont(SystemFontType.Bitmap, new IntPtr(2));
		public static Font Bitmap9By15 { get { return mvarBitmap9By15; } }
		
		private static Font mvarBitmap8By13 = new SystemFont(SystemFontType.Bitmap, new IntPtr(3));
		public static Font Bitmap8By13 { get { return mvarBitmap8By13; } }
		
		private static Font mvarBitmapTimesRoman10 = new SystemFont(SystemFontType.Bitmap, new IntPtr(4));
		public static Font BitmapTimesRoman10 { get { return mvarBitmapTimesRoman10; } }
		
		private static Font mvarBitmapTimesRoman24 = new SystemFont(SystemFontType.Bitmap, new IntPtr(5));
		public static Font BitmapTimesRoman24 { get { return mvarBitmapTimesRoman24; } }
		
		private static Font mvarBitmapHelvetica10 = new SystemFont(SystemFontType.Bitmap, new IntPtr(6));
		public static Font BitmapHelvetica10 { get { return mvarBitmapHelvetica10; } }
		
		private static Font mvarBitmapHelvetica12 = new SystemFont(SystemFontType.Bitmap, new IntPtr(7));
		public static Font BitmapHelvetica12 { get { return mvarBitmapHelvetica12; } }
		
		private static Font mvarBitmapHelvetica18 = new SystemFont(SystemFontType.Bitmap, new IntPtr(8));
		public static Font BitmapHelvetica18 { get { return mvarBitmapHelvetica18; } }
	}
	
	public enum SystemFontType
	{
		Stroke,
		Bitmap
	}
	public class SystemFont : Font
	{
		private SystemFontType mvarFontType = SystemFontType.Stroke;
		public SystemFontType FontType { get { return mvarFontType; } }
		
		private IntPtr mvarHandle = IntPtr.Zero;
		public IntPtr Handle { get { return mvarHandle; } }
		
		internal SystemFont(SystemFontType fontType, IntPtr handle)
		{
			mvarFontType = fontType;
			mvarHandle = handle;
		}
	}
	
	public class BitmapFont : Font
	{
		private TextureFontObjectModel mvarFont = new TextureFontObjectModel();
		public TextureFontObjectModel Font { get { return mvarFont; } }
		
		public BitmapFont (string FontFileName)
		{
			mvarFont.Clear ();
			Document.Load (mvarFont, new Caltron.DataFormats.TextureFont.TextureFontBinaryDataFormat(), new FileAccessor(FontFileName));
		}
	}
}

