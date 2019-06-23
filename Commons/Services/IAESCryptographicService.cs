using Commons;

namespace Commons
{
    public interface IAESCryptographicService
    {
        byte[] Decrypt(byte[] encryptedBuffer, AESKey aesKey);
        byte[] Encrypt(byte[] buffer, AESKey aesKey);
        AESKey GenerateKey();
    }
}