﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                [StatusBarMessage.MD5ChecksumGenerated] = "MD5 hashsum generated successful",
                [StatusBarMessage.MD5ChecksumOpened] = "MD5 hashsum opened successful",
                [StatusBarMessage.MD5ChecksumhSaved] = "MD5 hashsum saved successful",
            };

        public string this[StatusBarMessage message]
        {
            get => msg[message];
        }
    }
}
