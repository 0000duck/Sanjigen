using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public class Camera
    {
        private Vector3 mvarPosition = Vector3.Empty;
        public Vector3 Position
        {
            get { return mvarPosition; }
            set { mvarPosition = value; }
        }
    }
}
