using Commons.Models;

namespace Commons.Services
{
    public interface IAESCryptographicService
    {
        byte[] Decrypt(BufferFrame encryptedBuffer, AESKey aesKey);
        BufferFrame Encrypt(byte[] buffer, AESKey aesKey);
        AESKey GenerateKey();
    }
}