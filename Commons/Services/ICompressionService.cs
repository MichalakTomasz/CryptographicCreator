using System.Security.Cryptography;

namespace Commons
{
    public interface ICompressionService
    {
        BufferFrame Compress(byte[] source);
        BufferFrame Compress(RSAParameters rsaParameters);
        byte[] DecompressByteBuffer(BufferFrame archiveFrame);
        RSAParameters DecompressRSAParameters(BufferFrame archiveFrame);
            }
}