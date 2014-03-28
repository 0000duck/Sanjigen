using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron
{
    public static class Application
    {
        public static bool Initialize()
        {
            string[] args = new string[0];
            return Initialize(ref args);
        }
        public static bool Initialize(ref string[] args)
        {
            Internal.FreeGLUT.Methods.glutInit(ref args);
            Internal.FreeGLUT.Methods.glutInitDisplayMode(Internal.FreeGLUT.Constants.GLUT_DOUBLE | Internal.FreeGLUT.Constants.GLUT_RGBA | Internal.FreeGLUT.Constants.GLUT_DEPTH | Internal.FreeGLUT.Constants.GLUT_MULTISAMPLE | Internal.FreeGLUT.Constants.GLUT_ACCUM | Internal.FreeGLUT.Constants.GLUT_ALPHA);
            return true;
        }
        public static void Start()
        {
            Internal.FreeGLUT.Methods.glutMainLoop();
        }
        public static void Stop()
        {
            Internal.FreeGLUT.Methods.glutLeaveMainLoop();
        }
    }
}
