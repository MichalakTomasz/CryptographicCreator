using System;

namespace Commons
{
    [Serializable]
    public class ArchiveFrame
    {
        public int decompressedBufferLength { get; set; }
        public byte[] Buffer { get; set; }
    }
}
