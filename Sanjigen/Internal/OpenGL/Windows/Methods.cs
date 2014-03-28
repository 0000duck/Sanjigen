using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Caltron.Internal.OpenGL.Windows
{
	/// <summary>
	/// Description of GL.
	/// </summary>
	internal static class Methods
    {
        private const string LIBRARY_OPENGL = "opengl32.dll";

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glAccum(int operation, float value);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glAlphaFunc(Constants.GLAlphaFunc func, float value);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glBegin(int mode);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glBlendFunc(Constants.GLBlendFunc sourceFactorMode, Constants.GLBlendFunc destinationFactorMode);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glEnd();
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glEndList();
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glCallList(int list);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glClear(int mask);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glClearColor(float r, float g, float b, float a);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glClearColor(int r, int g, int b, int a);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glCullFace(Constants.GLFace face);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glFrontFace(Constants.GLFaceOrientation orientation);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glLightfv(int light, int pname, float[] parameters);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glMatrixMode(MatrixMode mode);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glNewList(int list, Internal.OpenGL.Constants.GLDisplayListMode mode);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glNormal3dv(double[] v);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glNormal3fv(float[] v);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex3fv(float[] v);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex3dv(double[] v);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glPushMatrix();
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glPopMatrix();
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glLoadIdentity();
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTranslated(double x, double y, double z);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTranslatef(float x, float y, float z);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glRotated(double angle, double x, double y, double z);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glRotatef(float angle, float x, float y, float z);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glScaled(double x, double y, double z);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glScalef(float x, float y, float z);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex2d(double x, double y);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex2f(float x, float y);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex2i(int x, int y);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex2s(short x, short y);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex2dv(double[] values);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex2fv(float[] values);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex2iv(int[] values);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex2sv(short[] values);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex3d(double x, double y, double z);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex3f(float x, float y, float z);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex3i(int x, int y, int z);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex3s(short x, short y, short z);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex4d(double x, double y, double z, double w);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex4f(float x, float y, float z, float w);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex4i(int x, int y, int z, int w);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertex4s(short x, short y, short z, short w);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glEnableClientState(int state);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glDisableClientState(int state);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glEnable(Constants.GLCapabilities capability);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glDisable(Constants.GLCapabilities capability);
        [DllImport(LIBRARY_OPENGL)]
        public static extern bool glIsEnabled(Constants.GLCapabilities capability);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glEnable(int capability);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glDisable(int capability);
        [DllImport(LIBRARY_OPENGL)]
        public static extern bool glIsEnabled(int capability);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glBindTexture(TextureTarget target, uint texture);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glPixelStorei(int a, int b);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexParameteri(Constants.TextureParameterTarget target, Constants.TextureParameterName pname, int param);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexParameterf(Constants.TextureParameterTarget target, Constants.TextureParameterName pname, float param);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexEnvf(Constants.TextureEnvironmentTarget target, Constants.TextureEnvironmentParameterName pname, float param);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexEnvi(Constants.TextureEnvironmentTarget target, Constants.TextureEnvironmentParameterName pname, int param);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord1s(short s);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord1i(int s);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord1f(float s);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord1d(double s);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord2s(short u, short v);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord2i(int u, int v);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord2f(float u, float v);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord2d(double u, double v);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord3s(short s, short t, short r);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord3i(int s, int t, int r);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord3f(float s, float t, float r);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord3d(double s, double t, double r);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord4s(short s, short t, short r, short q);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord4i(int s, int t, int r, int q);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord4f(float s, float t, float r, float q);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord4d(double s, double t, double r, double q);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord1sv(short[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord1iv(int[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord1fv(float[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord1dv(double[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord2sv(short[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord2iv(int[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord2fv(float[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord2dv(double[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord3sv(short[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord3iv(int[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord3fv(float[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord3dv(double[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord4sv(short[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord4iv(int[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord4fv(float[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoord4dv(double[] coords);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glDrawElements(int mode, int count, Internal.OpenGL.Constants.GLElementType type, byte[] indices);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glDrawElements(int mode, int count, Internal.OpenGL.Constants.GLElementType type, ushort[] indices);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glDrawElements(int mode, int count, Internal.OpenGL.Constants.GLElementType type, uint[] indices);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glVertexPointer(int size, int type, int stride, IntPtr ptr);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glNormalPointer(int type, int stride, IntPtr ptr);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexCoordPointer(int size, int type, int stride, IntPtr ptr);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexGend(int coord, int pname, double param);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexGenf(int coord, int pname, float param);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexGeni(int coord, int pname, int param);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexGendv(int coord, int pname, double[] param);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexGenfv(int coord, int pname, float[] param);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexGeniv(int coord, int pname, int[] param);
		
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glColor4b(byte red, byte green, byte blue, byte alpha);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glColor4d(double red, double green, double blue, double alpha);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glColor4f(float red, float green, float blue, float alpha);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glColor4i(int red, int green, int blue, int alpha);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glColor4s(short red, short green, short blue, short alpha);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glColor4ub(byte red, byte green, byte blue, byte alpha);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glColor4ui(uint red, uint green, uint blue, uint alpha);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glColor4us(ushort red, ushort green, ushort blue, ushort alpha);
		
		[DllImport(LIBRARY_OPENGL)]
        public static extern void glMaterialf(Constants.GLFace face, int pname, float param);
		[DllImport(LIBRARY_OPENGL)]
        public static extern void glMateriali(Constants.GLFace face, int pname, int param);
		[DllImport(LIBRARY_OPENGL)]
        public static extern void glMaterialfv(Constants.GLFace face, int pname, float[] param);
		[DllImport(LIBRARY_OPENGL)]
        public static extern void glMaterialiv(Constants.GLFace face, int pname, int[] param);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glRasterPos2d(double x, double y);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glRasterPos2f(float x, float y);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glRasterPos2i(int x, int y);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glRasterPos2s(short x, short y);
		
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glLineWidth(float lineWidth);
		
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glMultMatrixd(double[] m);
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glMultMatrixf(float[] m);
		
		[DllImport(LIBRARY_OPENGL)]
		public static extern void glFlush();
		
		[DllImport(LIBRARY_OPENGL)]
		public static extern bool wglMakeCurrent(IntPtr hdc, IntPtr hglrc);
		[DllImport(LIBRARY_OPENGL)]
		public static extern IntPtr wglGetCurrentDC();
		[DllImport(LIBRARY_OPENGL)]
		public static extern IntPtr wglCreateContext(IntPtr hdc);
		[DllImport("gdi32.dll")]
		public static extern bool SwapBuffers(IntPtr hdc);
        
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glShadeModel(Constants.GLShadeModel mode);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glClearDepth(float d);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glDepthFunc(Constants.GLComparisonFunc func);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glHint(Constants.GLHintTarget target, Constants.GLHintMode mode);
        
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glViewport(int x, int y, int w, int h);

        [DllImport(LIBRARY_OPENGL)]
        public static extern int glGenLists(int range);
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glGenTextures(int textureCount, uint[] textureIDs);
        
        [DllImport(LIBRARY_OPENGL)]
        public static extern void glTexImage2D(TextureTarget target, int level, Constants.GLTextureInternalFormat internalFormat, int width, int height, int border, Constants.GLTextureFormat format, Constants.GLTextureType type, IntPtr pixelData);

        [DllImport(LIBRARY_OPENGL)]
        public static extern void glOrtho(double left, double right, double bottom, double top, double near, double far);
    }
}