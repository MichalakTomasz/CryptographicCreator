using System.Collections.Generic;

namespace CryptographicCreator.Models
{
    public class StatusBarMessages : IStatusBarMessages
    {
        private readonly Dictionary<StatusBarMessage, string> msg =
            new Dictionary<StatusBarMessage, string>
            {
                [StatusBarMessage.None] = string.Empty,
                [StatusBarMessage.RSAKeysGenerated] = "RSA keys generated successful",
                [StatusBarMessage.Canceled] = "Action canceled",
                [StatusBarMessage.RSAPrivateKeyGenerated] = "RSA private key created successful",
                [StatusBarMessage.RSAPrivateKeyOpened] = "RSA private opened successful",
                [StatusBarMessage.RSAPrivateKeySaved] = "RSA pivate key saved successful",
                [StatusBarMessage.RSAPublicKeyGenerated] = "RSA public key generated successful",
                [StatusBarMessage.RSAPublicKeyOpened] = "RSA public key opened successful",
                [StatusBarMessage.RSAPublicKeySaved] = "RSA public key saved successful",
                [StatusBarMessage.RSAEncryptedDataOpened] = "RSA encrypted data loaded successful",
                [StatusBarMessage.RSAEncryptedDataSaved] = "RSA encrypted data saved successful",
                [StatusBarMessage.RSADataEncrypted] = "RSA data encrypted successful",
                [StatusBarMessage.RSADataDecrypted] = "RSA data decrypted successful",
                [StatusBarMessage.AESKeyGenerated] = "AES data generated succesful",
                [StatusBarMessage.AESKeyOpened] = "AES key opened successful",
                [StatusBarMessage.AESKeySaved] = "AES key saved successful",
                [StatusBarMessage.AESEncryptedDataOpened] = "AES encrypted data opened successful",
                [StatusBarMessage.AESEncryptedDataSaved] = "AES encrypted data saved successful",
                [StatusBarMessage.AESDataEncrypted] = "AES data encrypted successful",
                [StatusBarMessage.MD5ChecksumGenerated] = "MD5 checksum generated successful",
                [StatusBarMessage.MD5ChecksumOpened] = "MD5 checksum opened successful",
                [StatusBarMessage.MD5ChecksumhSaved] = "MD5 checksum saved successful",
                [StatusBarMessage.SHA256ChecksumGenerated] = "SHA256 checksum generated successful",
                [StatusBarMessage.SHA256ChecksumOpened] = "SHA256 checksum opened successful",
                [StatusBarMessage.SHA256ChecksumhSaved] = "SHA256 checksum saved successful",
                [StatusBarMessage.SHA512ChecksumGenerated] = "SHA512 checksum generated successful",
                [StatusBarMessage.SHA512ChecksumOpened] = "SHA512 checksum opened successful",
                [StatusBarMessage.SHA512ChecksumhSaved] = "SHA512 checksum saved successful",
            };

        public string this[StatusBarMessage message]
            => msg[message];
    }
}
