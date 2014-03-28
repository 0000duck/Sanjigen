using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

using UniversalEditor.ObjectModels.PMAXPatch;
using UniversalEditor.DataFormats.PMAXPatch;
using UniversalEditor.Accessors;

namespace Caltron.PMAXPatcher
{
	class Program
	{
		static void Main(string[] args)
		{
			string FileTitle = System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
			if (args.Length == 0)
			{
				Console.WriteLine(FileTitle + ": no input files");
				return;
			}

			Console.WriteLine("PMAX Patcher v0.1a - patches PMD to PMA model file format");


			string InputFileName = args[0];
			string OutputFileName = System.IO.Path.ChangeExtension(InputFileName, ".pma");

			PMAXPatchObjectModel patches = new PMAXPatchObjectModel();

			Document.Load(patches, new PMAXPatchXMLDataFormat(), new FileAccessor(InputFileName), true);
			
			string basePath = System.IO.Path.GetDirectoryName(InputFileName);
			if (String.IsNullOrEmpty(basePath))
			{
				basePath = Environment.CurrentDirectory;
			}

			foreach (PMAXPatch patch in patches.Patches)
			{
				PMAXPatchObjectModel patches2 = new PMAXPatchObjectModel();
				patches2.Patches.Add(patch);

				MemoryAccessor mem = new MemoryAccessor();
				Document.Save(patches2, new PMAXPatchBinaryDataFormat(), mem, true);

				byte[] patchBytes = mem.ToArray();
				byte[] originalFileBytes = System.IO.File.ReadAllBytes(basePath + System.IO.Path.DirectorySeparatorChar.ToString() + patch.InputFileName);
				byte[] modifiedFileBytes = new byte[originalFileBytes.Length + patchBytes.Length];
				
				Array.Copy(originalFileBytes, 0, modifiedFileBytes, 0, originalFileBytes.Length);
				Array.Copy(patchBytes, 0, modifiedFileBytes, originalFileBytes.Length, patchBytes.Length);

				System.IO.File.WriteAllBytes(basePath + System.IO.Path.DirectorySeparatorChar.ToString() + patch.OutputFileName, modifiedFileBytes);
			}
		}
	}
}
