using System;

namespace Commons
{
    [Serializable]
    public class ArchiveFrame
    {
        public int DecompressedBufferLength { get; set; }
        public byte[] Buffer { get; set; }
    }
}
