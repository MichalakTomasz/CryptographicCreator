using System;
using System.Collections.Generic;
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
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaParameters);
                    return rsa.Encrypt(data, true);
                }
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }

        public byte[] Encrypt(string text, Encoding encoding, RSAParameters rsaParameters)
        {
            try
            {
                var rsa = new RSACryptoServiceProvider();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    var encryptedByteArray = encoding.GetBytes(text);
                    rsa.ImportParameters(rsaParameters);
                    return rsa.Encrypt(encryptedByteArray, true);
                }
                else return default(byte[]);
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }

        public byte[] Decrypt(byte[] data, RSAParameters rsaParameters)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaParameters);
                    return rsa.Decrypt(data, true);
                }
            }
            catch (Exception)
            {
                return default(byte[]);
            }
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
                return default(RSAPairKeyParameters);
            }
        }

        #endregion//Public Metchods
    }
}
