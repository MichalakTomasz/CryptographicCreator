using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Commons
{
    public class RSAGZipCompressionService : ICompressionService
    {
        public ArchiveFrame Compress(byte[] source)
        {
            try
            {
                using (var compressedStresm = new MemoryStream())
                {
                    using (var gZipStream = new GZipStream(compressedStresm, CompressionMode.Compress))
                    using (var entryStream = new MemoryStream(source))
                        entryStream.CopyTo(gZipStream);

                    return new ArchiveFrame
                    {
                        decompressedBufferLength = source.Length,
                        Buffer = compressedStresm.ToArray()
                    };
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GZip compression error: {e.Message}");
                return default(ArchiveFrame);
            }
        }

        public ArchiveFrame Compress(RSAParameters rsaParameters)
        {
            try
            {
                int entryBuferLength = 0;
                var RSAserializable = new RSASerializable
                {
                    D = rsaParameters.D,
                    DP = rsaParameters.DP,
                    DQ = rsaParameters.DQ,
                    Exponent = rsaParameters.Exponent,
                    InverseQ = rsaParameters.InverseQ,
                    Modulus = rsaParameters.Modulus,
                    P = rsaParameters.P,
                    Q = rsaParameters.Q
                };
                using (var compressedStream = new MemoryStream())
                {
                    using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                    using (var RSAParameterStream = new MemoryStream())
                    {
                        var binaryFormater = new BinaryFormatter();
                        binaryFormater.Serialize(RSAParameterStream, RSAserializable);
                        var seriailzedBuffer = RSAParameterStream.ToArray();
                        entryBuferLength = seriailzedBuffer.Length;
                        gZipStream.Write(seriailzedBuffer, 0, seriailzedBuffer.Length);
                    }
                    return new ArchiveFrame
                    {
                        decompressedBufferLength = entryBuferLength,
                        Buffer = compressedStream.ToArray()
                    };
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GZip Compression exception: {e.Message}");
                return default(ArchiveFrame);
            }
        }

        public RSAParameters DecompressRSAParameters(ArchiveFrame compressedData)
        {
            try
            {
                using (var memoryStream = new MemoryStream(compressedData.Buffer))
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    var decompressedArray = new byte[compressedData.decompressedBufferLength];
                    gZipStream.Read(decompressedArray, 0, compressedData.decompressedBufferLength);
                    using (var decompressedStream = new MemoryStream(decompressedArray))
                    {
                        var binaryFormatter = new BinaryFormatter();
                        var RSASerializable = (RSASerializable)binaryFormatter
                            .Deserialize(decompressedStream);
                        return new RSAParameters()
                        {
                            D = RSASerializable.D,
                            DP = RSASerializable.DP,
                            DQ = RSASerializable.DQ,
                            Exponent = RSASerializable.Exponent,
                            InverseQ = RSASerializable.InverseQ,
                            Modulus = RSASerializable.Modulus,
                            P = RSASerializable.P,
                            Q = RSASerializable.Q
                        };
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GZip decompression exception: {e.Message}");
                return default(RSAParameters);
            }
        }

        public byte[] DecompressByteBuffer(ArchiveFrame compressedData)
        {
            try
            {
                using (var compressedStream = new MemoryStream(compressedData.Buffer))
                using (var gZipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                using (var decompressedStream = new MemoryStream())
                {
                    gZipStream.CopyTo(decompressedStream);
                    return decompressedStream.ToArray();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"GZip decompression exception: {e.Message}");
                return default(byte[]);
            }
        }

        [Serializable]
        private class RSASerializable
        {
            public byte[] D { get; set; }
            public byte[] DP { get; set; }
            public byte[] DQ { get; set; }
            public byte[] Exponent { get; set; }
            public byte[] InverseQ { get; set; }
            public byte[] Modulus { get; set; }
            public byte[] P { get; set; }
            public byte[] Q { get; set; }
        }
    }
}
