using System;

namespace Commons
{
    [Serializable]
    public class BufferFrame
    {
        public int OriginalBufferLength { get; set; }
        public byte[] Buffer { get; set; }
    }
}
