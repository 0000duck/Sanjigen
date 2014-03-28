using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public static class World
    {
        private static LightCollection mvarLights = new LightCollection();
        public static LightCollection Lights { get { return mvarLights; } }

    }
}
