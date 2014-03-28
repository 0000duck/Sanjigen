using System;
namespace Caltron
{
    public enum ColorMode
    {
        Byte,
        Float,
        Double
    }
	public struct Color : IComparable<Color>
	{
		private double mvarRed;
		public double Red { get { return mvarRed; } set { mvarRed = value; } }

        private double mvarGreen;
        public double Green { get { return mvarGreen; } set { mvarGreen = value; } }

        private double mvarBlue;
        public double Blue { get { return mvarBlue; } set { mvarBlue = value; } }

        private double mvarAlpha;
        public double Alpha { get { return mvarAlpha; } set { mvarAlpha = value; } }

        private ColorMode mvarMode;
        public ColorMode Mode { get { return mvarMode; } }
		
		public Color(byte red, byte green, byte blue) : this(red, green, blue, 255)
		{
		}
		public Color(byte red, byte green, byte blue, byte alpha)
		{
			mvarRed = red;
			mvarGreen = green;
			mvarBlue = blue;
			mvarAlpha = alpha;
            mvarMode = ColorMode.Byte;
        }
        public Color(float red, float green, float blue)
            : this(red, green, blue, 255)
        {
        }
        public Color(float red, float green, float blue, float alpha)
        {
            mvarRed = red;
            mvarGreen = green;
            mvarBlue = blue;
            mvarAlpha = alpha;
            mvarMode = ColorMode.Float;
        }
        public Color(double red, double green, double blue)
            : this(red, green, blue, 255)
        {
        }
        public Color(double red, double green, double blue, double alpha)
        {
            mvarRed = red;
            mvarGreen = green;
            mvarBlue = blue;
            mvarAlpha = alpha;
            mvarMode = ColorMode.Double;
        }

        public float[] ToFloatRGB()
        {
            switch (mvarMode)
            {
                case ColorMode.Float:
                case ColorMode.Double:
                {
                    return new float[] { (float)mvarRed, (float)mvarGreen, (float)mvarBlue };
                }
                case ColorMode.Byte:
                {
                    return new float[] { (float)(mvarRed / 255), (float)(mvarGreen / 255), (float)(mvarBlue / 255) };
                }
            }
            throw new InvalidOperationException("Invalid mode");
        }
        public float[] ToFloatRGBA()
        {
            switch (mvarMode)
            {
                case ColorMode.Float:
                case ColorMode.Double:
                {
                    return new float[] { (float)mvarRed, (float)mvarGreen, (float)mvarBlue, (float)mvarAlpha };
                }
                case ColorMode.Byte:
                {
                    return new float[] { ((float)mvarRed / 255), ((float)mvarGreen / 255), ((float)mvarBlue / 255), ((float)mvarAlpha / 255) };
                }
            }
            throw new InvalidOperationException("Invalid mode");
        }

        public int ToInt32()
        {
            byte a = (byte)(mvarAlpha * 255);
            byte r = (byte)(mvarRed * 255);
            byte g = (byte)(mvarGreen * 255);
            byte b = (byte)(mvarBlue * 255);
            return (((a | (r << 8)) | (g << 0x10)) | (b << 0x18));
        }

        public int CompareTo(Color other)
        {
            int thisVal = ToInt32();
            int otherVal = other.ToInt32();
            return thisVal.CompareTo(otherVal);
        }

        public static bool operator ==(Color left, Color right)
        {
            return (left.ToInt32() == right.ToInt32());
        }
        public static bool operator !=(Color left, Color right)
        {
            return (left.ToInt32() != right.ToInt32());
        }
    }
	public static class Colors
	{
		private static Color mvarBlack = new Color(0, 0, 0);
		public static Color Black { get { return mvarBlack; } }
		private static Color mvarBlue = new Color(0, 0, 255);
		public static Color Blue { get { return mvarBlue; } }
		private static Color mvarGreen = new Color(0, 255, 0);
		private static Color mvarCyan = new Color(0, 255, 255);
		public static Color Cyan { get { return mvarCyan; } }
		public static Color Green { get { return mvarGreen; } }
		private static Color mvarMagenta = new Color(255, 0, 255);
		public static Color Magenta { get { return mvarMagenta; } }
		private static Color mvarRed = new Color(255, 0, 0);
		public static Color Red { get { return mvarRed; } }
		private static Color mvarWhite = new Color(255, 255, 255);
		public static Color White { get { return mvarWhite; } }
		private static Color mvarYellow = new Color(255, 255, 0);
		public static Color Yellow { get { return mvarYellow; } }
	}
}

