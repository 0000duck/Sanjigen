using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.ObjectModels.GLVMExecutable
{
    public class GLVMExecutableResourceSet : ICloneable
    {
        public class GLVMExecutableResourceSetCollection
            : System.Collections.ObjectModel.Collection<GLVMExecutableResourceSet>
        {
            private Dictionary<string, GLVMExecutableResourceSet> setsByName = new Dictionary<string, GLVMExecutableResourceSet>();

            public GLVMExecutableResourceSet Add(string name, byte[] data)
            {
                GLVMExecutableResourceSet set = new GLVMExecutableResourceSet();
                set.Name = name;
                set.Data = data;
                base.Add(set);
                return set;
            }

            public GLVMExecutableResourceSet this[string name]
            {
                get
                {
                    if (setsByName.ContainsKey(name))
                    {
                        return setsByName[name];
                    }
                    return null;
                }
            }

            protected override void InsertItem(int index, GLVMExecutableResourceSet item)
            {
                setsByName.Add(item.Name, item);
                base.InsertItem(index, item);
            }
            protected override void RemoveItem(int index)
            {
                if (setsByName.ContainsKey(this[index].Name))
                {
                    setsByName.Remove(this[index].Name);
                }
                base.RemoveItem(index);
            }
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private byte[] mvarData = new byte[0];
        public byte[] Data { get { return mvarData; } set { mvarData = value; } }

        public object Clone()
        {
            GLVMExecutableResourceSet clone = new GLVMExecutableResourceSet();
            clone.Name = (mvarName.Clone() as string);
            clone.Data = (mvarData.Clone() as byte[]);
            return clone;
        }
    }
}
