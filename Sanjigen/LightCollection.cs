using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace Caltron
{
    public class LightCollection
    {
        Light[] mvarLights = new Light[7];
        public LightCollection()
        {
            for (int i = 0; i < mvarLights.Length; i++)
            {
                mvarLights[i] = new Light(i);
            }
        }

        public Light this[int index]
        {
            get
            {
                if (!(index < 8 && index > -1))
                {
                    throw new IndexOutOfRangeException("Light index must be between 0 and 7, inclusive");
                }
                return mvarLights[index];
            }
        }
    }
    public class Light
    {
        private int mvarIndex = 0;
        public int Index { get { return mvarIndex; } }

        private const int GL_LIGHT = 16384;
        public Light(int index)
        {
            mvarIndex = index;
        }

        private PositionVector4 mvarPosition = new PositionVector4(0, 0, 0, 0);
        public PositionVector4 Position
        {
            get { return mvarPosition; }
            set
            {
                mvarPosition = value;

                Internal.OpenGL.Methods.glLightfv(GL_LIGHT + mvarIndex, Internal.OpenGL.Constants.GL_POSITION, mvarPosition.ToFloatArray());
            }
        }
        private Color mvarDiffuseColor = Colors.White;
        public Color DiffuseColor
        {
            get { return mvarDiffuseColor; }
            set
            {
                mvarDiffuseColor = value;

                Internal.OpenGL.Methods.glLightfv(GL_LIGHT + mvarIndex, Internal.OpenGL.Constants.GL_DIFFUSE, mvarDiffuseColor.ToFloatRGBA());

            }
        }
        private Color mvarAmbientColor = Colors.White;
        public Color AmbientColor
        {
            get { return mvarAmbientColor; }
            set
            {
                mvarAmbientColor = value;

                Internal.OpenGL.Methods.glLightfv(GL_LIGHT + mvarIndex, Internal.OpenGL.Constants.GL_AMBIENT, mvarAmbientColor.ToFloatRGBA());
            }
        }
        private Color mvarSpecularColor = Colors.White;
        public Color SpecularColor
        {
            get { return mvarSpecularColor; }
            set
            {
                mvarSpecularColor = value;

                Internal.OpenGL.Methods.glLightfv(GL_LIGHT + mvarIndex, Internal.OpenGL.Constants.GL_SPECULAR, mvarSpecularColor.ToFloatRGBA());
            }
        }

        private bool mvarEnabled = false;
        public bool Enabled
        {
            get { return mvarEnabled; }
            set
            {
                mvarEnabled = value;
                if (mvarEnabled)
                {
                    Internal.OpenGL.Methods.glEnable(GL_LIGHT + mvarIndex);
                }
                else
                {
                    Internal.OpenGL.Methods.glDisable(GL_LIGHT + mvarIndex);
                }
            }
        }
    }
}
