using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographicCreator.Models
{
    public enum StatusBarMessage
    {
        None,
        Canceled,
        RSAKeysGenerated,
        RSAPrivateKeyGenerated,
        RSAPrivateKeyOpened,
        RSAPrivateKeySaved,
        RSAPublicKeyGenerated,
        RSAPublicKeyOpened,
        RSAPublicKeySaved,
        RSAEncryptedDataOpened,
        RSAEncryptedDataSaved,
        RSADataEncrypted,
        RSADataDecrypted,
        AESKeyGenerated,
        AESKeyOpened,
        AESKeySaved,
        AESEncryptedDataOpened,
        AESEncryptedDataSaved,
        AESDataEncrypted,
        AESDataDecrypted,
        MD5HashOpened,
        MD5HashSaved,
        MD5HashGenerated
    }
}
