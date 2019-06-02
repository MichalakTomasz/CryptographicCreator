using System.Security.Cryptography;
using System.Text;

namespace Commons
{
    public interface IRSACryptographicService
    {
        byte[] Decrypt(byte[] data, RSAParameters rsaParameters);
        byte[] Encrypt(byte[] data, RSAParameters rsaParameters);
        byte[] Encrypt(string text, Encoding encoding, RSAParameters rsaParameters);
        RSAPairKeyParameters GenerateKeyParameters();
        bool CompareKeyBases(RSAParameters parameters1, RSAParameters parameters2);
    }
}