using System;
namespace Caltron
{
	public struct Dimension3D
	{
		private double mvarWidth;
		public double Width { get { return mvarWidth; } set { mvarWidth = value; } }
		
		private double mvarHeight;
		public double Height { get { return mvarHeight; } set { mvarHeight = value; } }
		
		private double mvarDepth;
		public double Depth { get { return mvarDepth; } set { mvarDepth = value; } }
		
		public Dimension3D(double width, double height, double depth)
		{
			mvarWidth = width;
			mvarHeight = height;
			mvarDepth = depth;
		}
		
	}
}

