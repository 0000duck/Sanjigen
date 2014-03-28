using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;
using UniversalEditor.IO;

using Caltron.Shading.ObjectModels.Shader;

namespace Caltron.Shading.DataFormats.Shader
{
    public class CaltronBinaryShaderDataFormat : DataFormat
    {
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Capabilities.Add(typeof(ShaderObjectModel), DataFormatCapabilities.All);
            dfr.Filters.Add("Cross-platform binary shader", new byte?[][] { new byte?[] { (byte)'X', (byte)'p', (byte)'l', (byte)'a', (byte)'t', (byte)'S', (byte)'h', (byte)'d' } }, new string[] { "*.xsb" });
            return dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            BinaryReader br = base.Stream.BinaryReader;
            string XplatShd = br.ReadFixedLengthString(8);
            short versionMajor = br.ReadInt16();
            short versionMinor = br.ReadInt16();

            int definitionCount = br.ReadInt32();
            for (int i = 0; i < definitionCount; i++)
            {

            }
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
