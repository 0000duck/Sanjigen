using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public class RenderMatrix
    {
        private IntPtr mvarHDC = IntPtr.Zero;
        private IntPtr mvarHGLRC = IntPtr.Zero;
        public RenderMatrix()
        {
        }
        public RenderMatrix(IntPtr hdc, IntPtr hglrc)
        {
            mvarHDC = hdc;
            mvarHGLRC = hglrc;
        }

        private MatrixMode mvarMode = MatrixMode.ModelView;
        public MatrixMode Mode
        {
            get { return mvarMode; }
            set
            {
                mvarMode = value;

                if (mvarHDC != IntPtr.Zero && mvarHGLRC != IntPtr.Zero) Internal.OpenGL.Methods.glMakeCurrent(mvarHDC, mvarHGLRC);
                Internal.OpenGL.Methods.glMatrixMode(value);
            }
        }

        public void Push()
        {
            if (mvarHDC != IntPtr.Zero && mvarHGLRC != IntPtr.Zero) Internal.OpenGL.Methods.glMakeCurrent(mvarHDC, mvarHGLRC);
            Internal.OpenGL.Methods.glPushMatrix();
        }
        public void Pop()
        {
            if (mvarHDC != IntPtr.Zero && mvarHGLRC != IntPtr.Zero) Internal.OpenGL.Methods.glMakeCurrent(mvarHDC, mvarHGLRC);
            Internal.OpenGL.Methods.glPopMatrix();
        }
        public void Reset()
        {
            if (mvarHDC != IntPtr.Zero && mvarHGLRC != IntPtr.Zero) Internal.OpenGL.Methods.glMakeCurrent(mvarHDC, mvarHGLRC);

            // Reset the current matrix
            Internal.OpenGL.Methods.glLoadIdentity();
        }

        public void Multiply(double[] values)
        {
            if (mvarHDC != IntPtr.Zero && mvarHGLRC != IntPtr.Zero) Internal.OpenGL.Methods.glMakeCurrent(mvarHDC, mvarHGLRC);
            Internal.OpenGL.Methods.glMultMatrixd(values);
        }
        public void Multiply(float[] values)
        {
            if (mvarHDC != IntPtr.Zero && mvarHGLRC != IntPtr.Zero) Internal.OpenGL.Methods.glMakeCurrent(mvarHDC, mvarHGLRC);
            Internal.OpenGL.Methods.glMultMatrixf(values);
        }
    }
}
