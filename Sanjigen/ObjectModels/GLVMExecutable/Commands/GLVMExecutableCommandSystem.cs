using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.ObjectModels.GLVMExecutable.Commands
{
    public enum GLVMExecutableSystemCallType
    {
        None = 0,
        Push = 10,
        LoadVar = 40,
        StoreVar = 41,
        Add = 51,
        Subtract = 52,
        Multiply = 53,
        Divide = 54
    }
    public class GLVMExecutableCommandSystem : GLVMExecutableCommand
    {
        private GLVMExecutableSystemCallType mvarFunctionName = GLVMExecutableSystemCallType.None;
        public GLVMExecutableSystemCallType FunctionName { get { return mvarFunctionName; } set { mvarFunctionName = value; } }

        public override object Clone()
        {
            GLVMExecutableCommandSystem clone = new GLVMExecutableCommandSystem();
            clone.FunctionName = mvarFunctionName;
            foreach (object obj in base.ParameterValues)
            {
                clone.ParameterValues.Add(obj);
            }
            return clone;
        }
    }
}
