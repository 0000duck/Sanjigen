using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;

namespace Caltron.ObjectModels.GLVMExecutable
{
    public class GLVMExecutableObjectModel : ObjectModel
    {
        private static ObjectModelReference _omr = null;
        public override ObjectModelReference MakeReference()
        {
            if (_omr == null)
            {
                _omr = base.MakeReference();
                _omr.Title = "OpenGL Virtual Machine executable";
            }
            return _omr;
        }

        private GLVMExecutableCommandSet.GLVMExecutableCommandSetCollection mvarCommandSets = new GLVMExecutableCommandSet.GLVMExecutableCommandSetCollection();
        public GLVMExecutableCommandSet.GLVMExecutableCommandSetCollection CommandSets { get { return mvarCommandSets; } }

        private GLVMExecutableResourceSet.GLVMExecutableResourceSetCollection mvarResourceSets = new GLVMExecutableResourceSet.GLVMExecutableResourceSetCollection();
        public GLVMExecutableResourceSet.GLVMExecutableResourceSetCollection ResourceSets { get { return mvarResourceSets; } }

        public override void Clear()
        {
            mvarCommandSets.Clear();
        }
        public override void CopyTo(ObjectModel where)
        {
            GLVMExecutableObjectModel clone = (where as GLVMExecutableObjectModel);
            if (clone == null) return;

            foreach (GLVMExecutableCommandSet set in mvarCommandSets)
            {
                clone.CommandSets.Add(set.Clone() as GLVMExecutableCommandSet);
            }
            foreach (GLVMExecutableResourceSet set in mvarResourceSets)
            {
                clone.ResourceSets.Add(set.Clone() as GLVMExecutableResourceSet);
            }
        }
    }
}
