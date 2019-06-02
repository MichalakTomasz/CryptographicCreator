using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public class RSACryptographicService : IRSACryptographicService
    {
        #region Public Methods

        public byte[] Encrypt(byte[] data, RSAParameters rsaParameters)
        {
            if (data.Length > 0 && 
                !rsaParameters.Equals(default(RSAParameters)))
            {
                try
                {
                    using (var rsa = new RSACryptoServiceProvider())
                    {
                        rsa.ImportParameters(rsaParameters);
                        return rsa.Encrypt(data, true);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"RSAEncryption exception: {e.Message}");
                    return default(byte[]);
                }
            }
            else return default(byte[]);
        }

        public byte[] Encrypt(string text, Encoding encoding, RSAParameters rsaParameters)
        {
            if (!string.IsNullOrWhiteSpace(text) && 
                !rsaParameters.Equals(default(RSAParameters)))
            {
                try
                {
                    using (var rsa = new RSACryptoServiceProvider())
                    {
                        var encryptedByteArray = encoding.GetBytes(text);
                        rsa.ImportParameters(rsaParameters);
                        return rsa.Encrypt(encryptedByteArray, true);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"RSAEncryption exception: {e.Message}");
                    return default(byte[]);
                }
            }
            else return default(byte[]);
        }

        public byte[] Decrypt(byte[] data, RSAParameters rsaParameters)
        {
            if (data.Length > 0 &&
                !rsaParameters.Equals(default(RSAParameters)))
            {
                try
                {
                    using (var rsa = new RSACryptoServiceProvider())
                    {
                        rsa.ImportParameters(rsaParameters);
                        return rsa.Decrypt(data, true);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"RSADecryption exception: {e.Message}");
                    return default(byte[]);
                }
            }
            return default(byte[]);
        }

        public RSAPairKeyParameters GenerateKeyParameters()
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    var publicAndPrivateKeyParameters = rsa.ExportParameters(false);
                    var privateKeyParameters = rsa.ExportParameters(true);
                    return new RSAPairKeyParameters(publicAndPrivateKeyParameters, privateKeyParameters);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"RSAEncryption exception: {e.Message}");
                return default(RSAPairKeyParameters);
            }
        }

        #endregion//Public Metchods
    }
}
