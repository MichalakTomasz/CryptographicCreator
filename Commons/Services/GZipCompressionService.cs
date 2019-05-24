using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class GZipCompressionService : ICompressionService
    {
        public bool Compress(byte[] source, string path)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    gZipStream.Write(source, 0, (int)source.Length);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public byte[] Decompress(string path)
        {
            try
            {
                using (var fileStream = new MemoryStream())
                using (var gZipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    var decompressedArray = new byte[fileStream.Length];
                    gZipStream.Read(decompressedArray, 0, (int)fileStream.Length);
                    return decompressedArray;
                }
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }
    }
}
