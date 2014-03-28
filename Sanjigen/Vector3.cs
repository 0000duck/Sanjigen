using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public struct Vector3
    {
        public static readonly Vector3 Empty;

        private double mvarX;
        public double X
        {
            get { return mvarX; }
            set { mvarX = value; }
        }

        private double mvarY;
        public double Y
        {
            get { return mvarY; }
            set { mvarY = value; }
        }

        private double mvarZ;
        public double Z
        {
            get { return mvarZ; }
            set { mvarZ = value; }
        }

        public Vector3(double x, double y, double z)
        {
            mvarX = x;
            mvarY = y;
            mvarZ = z;
        }
    }
}
