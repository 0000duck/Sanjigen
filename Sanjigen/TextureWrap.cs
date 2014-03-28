using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public enum TextureWrap
    {
        Clamp = Internal.OpenGL.Constants.GL_CLAMP,
        ClampToEdge = Internal.OpenGL.Constants.GL_CLAMP_TO_EDGE,
        Repeat = Internal.OpenGL.Constants.GL_REPEAT
    }
}
