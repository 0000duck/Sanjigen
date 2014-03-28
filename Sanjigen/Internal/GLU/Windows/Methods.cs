using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Caltron.Internal.GLU.Windows
{
    public static class Methods
    {
        [DllImport("glu32.dll")]
        public static extern void gluPerspective(double fovy, double aspect, double zNear, double zFar);
    }
}
