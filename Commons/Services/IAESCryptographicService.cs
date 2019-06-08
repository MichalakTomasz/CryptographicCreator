using Commons;

namespace Commons
{
    public interface IAESCryptographicService
    {
        byte[] Decrypt(BufferFrame encryptedBuffer, AESKey aesKey);
        BufferFrame Encrypt(byte[] buffer, AESKey aesKey);
        AESKey GenerateKey();
    }
}