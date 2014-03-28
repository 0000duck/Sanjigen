using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caltron.PMAXPatcher.ObjectModels
{
    public abstract class PMAXPatchChunk
    {
        private string mvarName = String.Empty;
        /// <summary>
        /// 8-letter identifier for this chunk.
        /// </summary>
        public abstract string Name { get { return mvarName; } }

        protected abstract byte[] ToByteArrayInternal();

        public byte[] ToByteArray()
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            UniversalEditor.IO.BinaryWriter bw = new UniversalEditor.IO.BinaryWriter(ms);

            bw.WriteFixedLengthString(mvarName, 4);

            byte[] dataBytes = ToByteArrayInternal();
            bw.Write(dataBytes.Length);
            bw.Write(dataBytes);

            return ms.ToArray();
        }


        public class PMAXPatchChunkCollection
            : System.Collections.ObjectModel.Collection<PMAXPatchChunk>
        {

        }
    }
}
