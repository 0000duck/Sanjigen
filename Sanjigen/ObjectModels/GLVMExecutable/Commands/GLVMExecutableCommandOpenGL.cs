using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.ObjectModels.GLVMExecutable.Commands
{
    public enum GLVMExecutableOpenGLCallType : byte
    {
        None = 0,
        Accum,
        AlphaFunc,
        Begin,
        BindTexture,
        BlendFunc,
        CallList,
        Clear,
        Color,
        CreateContext,
        CullFace,
        DepthFunc,
        Enable,
        EnableClientState,
        End,
        EndList,
        Flush,
        FrontFace,
        GenLists,
        GenTextures,
        GetCurrentDC,
        Hint,
        Light,
        LineWidth,
        LoadIdentity,
        MakeCurrent,
        Material,
        MatrixMode,
        MultMatrix,
        NewList,
        Normal,
        Ortho,
        PixelStore,
        PopMatrix,
        PushMatrix,
        RasterPos,
        Rotate,
        Scale,
        ShadeModel,
        SwapBuffers,
        TexCoord,
        TexEnv,
        TexGen,
        TexImage,
        TexParameter,
        Translate,
        Vertex,
        Viewport
    }
    public class GLVMExecutableCommandOpenGL : GLVMExecutableCommand
    {
        private GLVMExecutableOpenGLCallType mvarFunctionName = GLVMExecutableOpenGLCallType.None;
        public GLVMExecutableOpenGLCallType FunctionName { get { return mvarFunctionName; } set { mvarFunctionName = value; } }

        public override object Clone()
        {
            GLVMExecutableCommandOpenGL clone = new GLVMExecutableCommandOpenGL();
            clone.FunctionName = mvarFunctionName;
            foreach (object obj in base.ParameterValues)
            {
                clone.ParameterValues.Add(obj);
            }
            return clone;
        }
    }
}
