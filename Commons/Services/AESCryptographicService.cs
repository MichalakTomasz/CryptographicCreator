using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace Commons
{
    public class AESCryptographicService : IAESCryptographicService
    {
        public byte[] Encrypt(byte[] buffer, AESKey aesKey)
        {
            try
            {
                using (var aesAlgoritm = new AesCryptoServiceProvider())
                {
                    aesAlgoritm.Key = aesKey.Key;
                    aesAlgoritm.IV = aesKey.IV;
                    ICryptoTransform encryptor = aesAlgoritm.CreateEncryptor(aesAlgoritm.Key, aesAlgoritm.IV);
                    using (var encryptedMemoryStream = new MemoryStream())
                    using (var sourceStream = new MemoryStream(buffer))
                    {
                        using (var cryptoStream = new CryptoStream(encryptedMemoryStream, encryptor, CryptoStreamMode.Write))
                            sourceStream.CopyTo(cryptoStream);
                        return encryptedMemoryStream.ToArray();
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($"AES encrypt error: {e.Message}");
                return default(byte[]);
            }
        }

        public byte[] Decrypt(byte[] encryptedBuffer, AESKey aesKey)
        {
            try
            {
                using (var decryptedStream = new MemoryStream())
                {
                    using (var aesAlg = new AesCryptoServiceProvider())
                    {
                        aesAlg.Key = aesKey.Key;
                        aesAlg.IV = aesKey.IV;
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (var msDecrypt = new MemoryStream(encryptedBuffer))
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            csDecrypt.CopyTo(decryptedStream);
                            return decryptedStream.ToArray();
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
