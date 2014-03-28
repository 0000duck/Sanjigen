using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.DataFormats.VersatileContainer;
using UniversalEditor.ObjectModels.VersatileContainer;

using Caltron.ObjectModels.GLVMExecutable;
using Caltron.ObjectModels.GLVMExecutable.Commands;

namespace Caltron.DataFormats.GLVMExecutable
{
    public class GLVMExecutableDataFormat : VersatileContainerDataFormat
    {
        protected override void BeforeLoadInternal(Stack<UniversalEditor.ObjectModel> objectModels)
        {
            base.BeforeLoadInternal(objectModels);
            objectModels.Push(new FileSystemObjectModel());
        }
        protected override void AfterLoadInternal(Stack<UniversalEditor.ObjectModel> objectModels)
        {
            base.AfterLoadInternal(objectModels);
            FileSystemObjectModel fsom = (objectModels.Pop() as FileSystemObjectModel);
            if (fsom.Title != "(c)2012 ALCEXHIM ; Cobalt Virtual Machine compiled executable")
            {
                throw new DataFormatException(UniversalEditor.Localization.StringTable.ErrorDataFormatInvalid);
            }
        }
        protected override void BeforeSaveInternal(Stack<UniversalEditor.ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);
            
            GLVMExecutableObjectModel exe = (objectModels.Pop() as GLVMExecutableObjectModel);
            FileSystemObjectModel fsom = new FileSystemObjectModel();
            fsom.Title = "(c)2012 ALCEXHIM ; Cobalt Virtual Machine compiled executable";

            foreach (GLVMExecutableCommandSet set in exe.CommandSets)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                UniversalEditor.IO.BinaryWriter bw = new UniversalEditor.IO.BinaryWriter(ms);

                foreach (GLVMExecutableCommand cmd in set.Commands)
                {
                    if (cmd is GLVMExecutableCommandSystem)
                    {
                        bw.Write((byte)1);
                        GLVMExecutableCommandSystem syscmd = (cmd as GLVMExecutableCommandSystem);
                        switch (syscmd.FunctionName)
                        {
                            case GLVMExecutableSystemCallType.Push:
                            {
                                if (syscmd.ParameterValues.Count == 1)
                                {
                                    WriteVariantOpcode(bw, 10, syscmd.ParameterValues[0]);
                                }
                                break;
                            }
                            case GLVMExecutableSystemCallType.LoadVar:
                            {
                                if (syscmd.ParameterValues.Count == 1)
                                {
                                    bw.Write((byte)40);
                                    bw.Write((string)syscmd.ParameterValues[0]);
                                }
                                break;
                            }
                            case GLVMExecutableSystemCallType.StoreVar:
                            {
                                if (syscmd.ParameterValues.Count == 1)
                                {
                                    bw.Write((byte)41);
                                    bw.Write((string)syscmd.ParameterValues[0]);
                                }
                                break;
                            }
                            case GLVMExecutableSystemCallType.Add:
                            {
                                if (syscmd.ParameterValues.Count == 2)
                                {
                                    WriteVariantOpcode(bw, 50, syscmd.ParameterValues[0], syscmd.ParameterValues[1]);
                                }
                                break;
                            }
                        }
                    }
                    else if (cmd is GLVMExecutableCommandOpenGL)
                    {
                        GLVMExecutableCommandOpenGL cmdd = (cmd as GLVMExecutableCommandOpenGL);
                        for (int i = cmdd.ParameterValues.Count - 1; i > -1; i--)
                        {
                            WriteVariantOpcode(bw, 10, cmdd.ParameterValues[i]);
                        }
                        
                        bw.Write((byte)2);
                        bw.Write((byte)cmdd.FunctionName);
                    }
                }
                bw.Close();

                fsom.Files.Add("Execute__" + set.Name, ms.ToArray());
            }

            foreach (GLVMExecutableResourceSet set in exe.ResourceSets)
            {
                fsom.Files.Add("ResourceSet__" + set.Name, set.Data);
            }

            objectModels.Push(fsom);
        }

        private void WriteVariantOpcode(UniversalEditor.IO.BinaryWriter bw, byte startOpcode, params object[] objs)
        {
            if (objs.Length == 0) throw new InvalidOperationException();

            if (objs[0] is byte)
            {
                bw.Write((byte)(startOpcode + 1)); // PUSHB
                foreach (object obj in objs)
                {
                    bw.Write((byte)obj);
                }
            }
            else if (objs[0] is short)
            {
                bw.Write((byte)(startOpcode + 2)); // PUSHS
                foreach (object obj in objs)
                {
                    bw.Write((short)obj);
                }
            }
            else if (objs[0] is int)
            {
                bw.Write((byte)(startOpcode + 4)); // PUSHI
                foreach (object obj in objs)
                {
                    bw.Write((int)obj);
                }
            }
            else if (objs[0] is long)
            {
                bw.Write((byte)(startOpcode + 8)); // PUSHL
                foreach (object obj in objs)
                {
                    bw.Write((long)obj);
                }
            }
            else if (objs[0] is sbyte)
            {
                bw.Write((byte)(startOpcode + 11)); // PUSHSB
                foreach (object obj in objs)
                {
                    bw.Write((sbyte)obj);
                }
            }
            else if (objs[0] is ushort)
            {
                bw.Write((byte)(startOpcode + 12)); // PUSHUS
                foreach (object obj in objs)
                {
                    bw.Write((ushort)obj);
                }
            }
            else if (objs[0] is uint)
            {
                bw.Write((byte)(startOpcode + 14)); // PUSHUI
                foreach (object obj in objs)
                {
                    bw.Write((uint)obj);
                }
            }
            else if (objs[0] is ulong)
            {
                bw.Write((byte)(startOpcode + 18)); // PUSHUL
                foreach (object obj in objs)
                {
                    bw.Write((ulong)obj);
                }
            }
            else if (objs[0] is float)
            {
                bw.Write((byte)(startOpcode + 24)); // PUSHF
                foreach (object obj in objs)
                {
                    bw.Write((float)obj);
                }
            }
            else if (objs[0] is double)
            {
                bw.Write((byte)(startOpcode + 28)); // PUSHD
                foreach (object obj in objs)
                {
                    bw.Write((double)obj);
                }
            }
        }
    }
}
