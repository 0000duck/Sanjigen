using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.ObjectModels.GLVMExecutable.Commands
{
    public enum GLVMExecutableGLUCallType
    {
        None = 0,
        Perspective
    }
    public class GLVMExecutableCommandGLU : GLVMExecutableCommand
    {
        private GLVMExecutableGLUCallType mvarFunctionName = GLVMExecutableGLUCallType.None;
        public GLVMExecutableGLUCallType FunctionName { get { return mvarFunctionName; } set { mvarFunctionName = value; } }

        public override object Clone()
        {
            GLVMExecutableCommandGLU clone = new GLVMExecutableCommandGLU();
            clone.FunctionName = mvarFunctionName;
            foreach (object obj in base.ParameterValues)
            {
                clone.ParameterValues.Add(obj);
            }
            return clone;
        }
    }
}
