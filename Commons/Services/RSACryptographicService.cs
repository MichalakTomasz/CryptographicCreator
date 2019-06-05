using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

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

        public bool CompareKeyBases(RSAParameters parameters1, RSAParameters parameters2)
        {
            if (parameters1.Exponent.Length == parameters2.Exponent.Length &&
                parameters1.Modulus.Length == parameters2.Modulus.Length)
            {
                for (var i = 0; i < parameters1.Exponent.Length; i++)
                    if (!(parameters1.Exponent[i] == parameters2.Exponent[i])) return false;
                for (var i = 0; i < parameters1.Modulus.Length; i++)
                    if (!(parameters1.Modulus[i] == parameters2.Modulus[i])) return false;
            }
            else return false;
            
            return true;
        }

        #endregion//Public Methods
    }
}
