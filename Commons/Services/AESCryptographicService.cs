using Commons;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace Commons
{
    public class AESCryptographicService : IAESCryptographicService
    {
        public BufferFrame Encrypt(byte[] buffer, AESKey aesKey)
        {
            try
            {
                using (var awsAlgoritm = new AesCryptoServiceProvider())
                {
                    awsAlgoritm.Key = aesKey.Key; 
                    awsAlgoritm.IV = aesKey.IV;
                    ICryptoTransform encryptor = awsAlgoritm.CreateEncryptor(awsAlgoritm.Key, awsAlgoritm.IV);
                    using (var encryptedMemoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(encryptedMemoryStream, encryptor, CryptoStreamMode.Write))
                            cryptoStream.Write(buffer, 0, buffer.Length);

                        return new BufferFrame
                        {
                            Buffer = encryptedMemoryStream.ToArray(),
                            OriginalBufferLength = buffer.Length
                        };
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($"AES encrypt error: {e.Message}");
                return default(BufferFrame);
            }
        }

        public byte[] Decrypt(BufferFrame encryptedBuffer, AESKey aesKey)
        {
            try
            {
                using (var aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = aesKey.Key;
                    aesAlg.IV = aesKey.IV;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var msDecrypt = new MemoryStream(encryptedBuffer.Buffer))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            var length = encryptedBuffer.OriginalBufferLength;
                            byte[] decryptedData = new byte[length];
                            csDecrypt.Read(decryptedData, 0, length);
                            return decryptedData;
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($"AES decrypt errer: {e.Message}");
                return default(byte[]);
            }
        }

        public AESKey GenerateKey()
        {
            try
            {
                var aes = Aes.Create();
                return new AESKey { Key = aes.Key, IV = aes.IV };
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($"AES generate key errer: {e.Message}");
                return default(AESKey);
            }
        }
    }
}
