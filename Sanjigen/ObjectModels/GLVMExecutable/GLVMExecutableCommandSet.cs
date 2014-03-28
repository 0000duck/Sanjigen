using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.ObjectModels.GLVMExecutable
{
    public class GLVMExecutableCommandSet : ICloneable
    {
        public class GLVMExecutableCommandSetCollection
            : System.Collections.ObjectModel.Collection<GLVMExecutableCommandSet>
        {
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private GLVMExecutableCommand.GLVMExecutableCommandCollection mvarCommands = new GLVMExecutableCommand.GLVMExecutableCommandCollection();
        public GLVMExecutableCommand.GLVMExecutableCommandCollection Commands
        {
            get { return mvarCommands; }
        }

        public object Clone()
        {
            GLVMExecutableCommandSet clone = new GLVMExecutableCommandSet();
            clone.Name = (mvarName.Clone() as string);
            foreach (GLVMExecutableCommand cmd in mvarCommands)
            {
                clone.Commands.Add(cmd.Clone() as GLVMExecutableCommand);
            }
            return clone;
        }
    }
}
