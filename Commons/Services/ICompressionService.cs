using System.Security.Cryptography;

namespace Commons
{
    public interface ICompressionService
    {
        ArchiveFrame Compress(byte[] source);
        ArchiveFrame Compress(RSAParameters rsaParameters);
        byte[] DecompressByteBuffer(ArchiveFrame archiveFrame);
        RSAParameters DecompressRSAParameters(ArchiveFrame archiveFrame);
            }
}