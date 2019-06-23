using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Commons
{
    public class GZipCompressionService : IGZipCompressionService
    {
        public byte[] Compress(byte[] buffer)
        {
            try
            {
                using (var resultStream = new MemoryStream())
                {
                    byte[] compressedBuffer = null;
                    using (var compressedStream = new MemoryStream())
                    {
                        using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
                            gZipStream.Write(buffer, 0, buffer.Length);
                        compressedBuffer = compressedStream.ToArray();
                    }
                    using(var memoryStream = new MemoryStream())
                    using (var binaryWriter = new BinaryWriter(memoryStream))
                    {
                        binaryWriter.Write(buffer.Length);
                        binaryWriter.Write(compressedBuffer, 0, compressedBuffer.Length);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GZip compress error: {e.Message}");
                return default(byte[]);
            }
        }

        public byte[] Decompress(byte[] buffer)
        {
            try
            {
                int bufferLength = 0;
                byte[] compressedBuffer = null;
                byte[] decompressedBuffer = null;
                using (var memoryStream = new MemoryStream(buffer))
                using (var binaryReader = new BinaryReader(memoryStream))
                {
                    bufferLength = binaryReader.ReadInt32();
                    compressedBuffer = new byte[memoryStream.Length - 4];
                    binaryReader.Read(compressedBuffer, 0, compressedBuffer.Length);
                }
                using (var decompressedStream = new MemoryStream())
                {
                    using (var compressedStream = new MemoryStream(compressedBuffer))
                    {
                        decompressedBuffer = new byte[bufferLength];
                        using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress, true))
                            gZipStream.Read(decompressedBuffer, 0, bufferLength);
                        return decompressedBuffer;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GZip AES decompress bufferFrame exception: {e.Message}");
                return default(byte[]);
            }
        }
    }
}
