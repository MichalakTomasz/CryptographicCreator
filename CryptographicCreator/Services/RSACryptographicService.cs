using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptographicCreator.Services
{
    class RSACryptographicService
    {
        #region Constructors

        public RSACryptographicService()
            => rsa = new RSACryptoServiceProvider();

        public RSACryptographicService(int sizeOfTheKeyInBits)
            => rsa = new RSACryptoServiceProvider(sizeOfTheKeyInBits);

        #endregion//Constructors

        #region Static Methods

        public static byte[] Encryption(byte[] data, RSAParameters rsaKey, bool doOAPPsdding)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaKey);
                    return rsa.Encrypt(data, doOAPPsdding);
                }
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }
        public static byte[] Decryption(byte[] data, RSAParameters rsaKey, bool doOAPPadding)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(rsaKey);
                    return rsa.Decrypt(data, doOAPPadding);
                }
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }

        public static RSAPairKeyParameters GenerateKeyParameters()
        {
            try
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    var publicPairKeyParameters = rsa.ExportParameters(false);
                    var privatePairKeyParameters = rsa.ExportParameters(true);
                    return new RSAPairKeyParameters(publicPairKeyParameters, privatePairKeyParameters);
                }
            }
            catch (Exception)
            {
                return default(RSAPairKeyParameters);
            }
        }

        #endregion//Static Methods

        #region Properties

        public string Text { get; set; }
        public byte[] EncryptedByteArray { get; private set; }

        public RSAParameters PublicKeyParameters
        {
            get
            {
                try
                {
                    return rsa.ExportParameters(false);
                }
                catch (Exception)
                {
                    return default(RSAParameters);
                }
            }
        }

        public RSAParameters PrivateAndPublicKeyParameters
        {
            get
            {
                try
                {
                    return rsa.ExportParameters(true);
                }
                catch (Exception)
                {
                    return default(RSAParameters);
                }
            }
        }

        #endregion

        #region Public Methods

        public byte[] Encrypt(byte[] byteArray)
        {
            try
            {
                return rsa.Encrypt(byteArray, true);
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }

        public byte[] Encrypt(string text, Encoding encoding)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    Text = text;
                    EncryptedByteArray = encoding.GetBytes(Text);
                    return rsa.Encrypt(EncryptedByteArray, true);
                }
                else return default(byte[]);
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }

        public byte[] Decrypt()
        {
            try
            {
                if (EncryptedByteArray != default(byte[]))
                {
                    return rsa.Decrypt(EncryptedByteArray, true);
                }
                else return default(byte[]);
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }

        public string Decrypt(Encoding encoding)
        {
            try
            {
                if (EncryptedByteArray != default(byte[]))
                {
                    var decryptedByteArray = rsa.Decrypt(EncryptedByteArray, true);
                    return encoding.GetString(decryptedByteArray);
                }
                else return default(string);
            }
            catch (Exception)
            {
                return default(string);
            }
        }

        #endregion //public metchods

        #region private fields

        private RSACryptoServiceProvider rsa;

        #endregion //private fields
    }
}
