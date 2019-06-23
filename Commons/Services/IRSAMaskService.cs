using System.Security.Cryptography;

namespace Commons
{
    public interface IRSAMaskService
    {
        byte[] Mask(RSAParameters aesKey);
        RSAParameters Unmask(byte[] buffer);
    }
}