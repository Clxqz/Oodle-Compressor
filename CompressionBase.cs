using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClxqzCompressor
{
    public abstract class CompressionBase
    {
        public abstract byte[] Decompress(byte[] data, int decompressedSize);
        public abstract byte[] Compress(byte[] buffer);
    }
}
