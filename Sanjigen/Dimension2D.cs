using System;
namespace Caltron
{
	public struct Dimension2D
	{
		private double mvarWidth;
		public double Width { get { return mvarWidth; } set { mvarWidth = value; } }
		
		private double mvarHeight;
		public double Height { get { return mvarHeight; } set { mvarHeight = value; } }
		
		public Dimension2D(double width, double height)
		{
			mvarWidth = width;
			mvarHeight = height;
		}
		
	}
}

