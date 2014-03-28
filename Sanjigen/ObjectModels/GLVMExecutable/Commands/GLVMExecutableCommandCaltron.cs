using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.ObjectModels.GLVMExecutable.Commands
{
    public enum GLVMExecutableCaltronCallType
    {
        None = 0,
        SetWorldLightPosition,
        SetWorldLightDiffuseColor,
        SetWorldLightAmbientColor,
        SetWorldLightSpecularColor,
        SetWorldLightEnabled
    }
    public class GLVMExecutableCommandCaltron : GLVMExecutableCommand
    {
        private GLVMExecutableCaltronCallType mvarFunctionName = GLVMExecutableCaltronCallType.None;
        public GLVMExecutableCaltronCallType FunctionName { get { return mvarFunctionName; } set { mvarFunctionName = value; } }

        public override object Clone()
        {
            GLVMExecutableCommandCaltron clone = new GLVMExecutableCommandCaltron();
            clone.FunctionName = mvarFunctionName;
            foreach (object obj in base.ParameterValues)
            {
                clone.ParameterValues.Add(obj);
            }
            return clone;
        }
    }
}
