using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Caltron.ObjectModels.GLVMExecutable.Commands;

namespace Caltron.ObjectModels.GLVMExecutable
{
    public abstract class GLVMExecutableCommand : ICloneable
    {
        public class GLVMExecutableCommandCollection
            : System.Collections.ObjectModel.Collection<GLVMExecutableCommand>
        {
            public GLVMExecutableCommandOpenGL Add(GLVMExecutableOpenGLCallType type, params object[] values)
            {
                GLVMExecutableCommandOpenGL call = new GLVMExecutableCommandOpenGL();
                call.FunctionName = type;
                foreach (object obj in values)
                {
                    call.ParameterValues.Add(obj);
                }
                base.Add(call);
                return call;
            }
            public GLVMExecutableCommandSystem Add(GLVMExecutableSystemCallType type, params object[] values)
            {
                GLVMExecutableCommandSystem call = new GLVMExecutableCommandSystem();
                call.FunctionName = type;
                foreach (object obj in values)
                {
                    call.ParameterValues.Add(obj);
                }
                base.Add(call);
                return call;
            }
            public GLVMExecutableCommandGLU Add(GLVMExecutableGLUCallType type, params object[] values)
            {
                GLVMExecutableCommandGLU call = new GLVMExecutableCommandGLU();
                call.FunctionName = type;
                foreach (object obj in values)
                {
                    call.ParameterValues.Add(obj);
                }
                base.Add(call);
                return call;
            }
            public GLVMExecutableCommandGLUT Add(GLVMExecutableGLUTCallType type, params object[] values)
            {
                GLVMExecutableCommandGLUT call = new GLVMExecutableCommandGLUT();
                call.FunctionName = type;
                foreach (object obj in values)
                {
                    call.ParameterValues.Add(obj);
                }
                base.Add(call);
                return call;
            }
            public GLVMExecutableCommandCaltron Add(GLVMExecutableCaltronCallType type, params object[] values)
            {
                GLVMExecutableCommandCaltron call = new GLVMExecutableCommandCaltron();
                call.FunctionName = type;
                foreach (object obj in values)
                {
                    call.ParameterValues.Add(obj);
                }
                base.Add(call);
                return call;
            }
        }

        private List<object> mvarParameterValues = new List<object>();
        public List<object> ParameterValues { get { return mvarParameterValues; } }


        public abstract object Clone();
    }
}
