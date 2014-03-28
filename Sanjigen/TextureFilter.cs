using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public enum TextureFilter
    {
        /// <summary>
        /// Returns the value of the texture element that is nearest (in Manhattan distance) to the center of the pixel being textured. 
        /// </summary>
        Nearest = Internal.OpenGL.Constants.GL_NEAREST,
        /// <summary>
        /// Returns the weighted average of the four texture elements that are closest to the center of the pixel being textured. These can include border texture elements, depending on the values of GL_TEXTURE_WRAP_S and GL_TEXTURE_WRAP_T, and on the exact mapping. 
        /// </summary>
        Linear = Internal.OpenGL.Constants.GL_LINEAR,
        /// <summary>
        /// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the GL_NEAREST criterion (the texture element nearest to the center of the pixel) to produce a texture value.
        /// </summary>
        NearestMipmapNearest = Internal.OpenGL.Constants.GL_NEAREST_MIPMAP_NEAREST,
        /// <summary>
        /// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the GL_LINEAR criterion (a weighted average of the four texture elements that are closest to the center of the pixel) to produce a texture value.
        /// </summary>
        LinearMipmapNearest = Internal.OpenGL.Constants.GL_LINEAR_MIPMAP_NEAREST,
        /// <summary>
        /// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses the GL_NEAREST criterion (the texture element nearest to the center of the pixel) to produce a texture value from each mipmap. The final texture value is a weighted average of those two values.
        /// </summary>
        NearestMipmapLinear = Internal.OpenGL.Constants.GL_NEAREST_MIPMAP_LINEAR,
        /// <summary>
        /// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses the GL_LINEAR criterion (a weighted average of the four texture elements that are closest to the center of the pixel) to produce a texture value from each mipmap. The final texture value is a weighted average of those two values.
        /// </summary>
        LinearMipmapLinear = Internal.OpenGL.Constants.GL_LINEAR_MIPMAP_LINEAR
    }
}
