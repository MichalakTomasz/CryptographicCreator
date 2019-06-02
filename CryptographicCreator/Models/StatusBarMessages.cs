using System;
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
                [StatusBarMessage.RSAKeysGenerated] = "RSA keys was generated",
                [StatusBarMessage.Canceled] = "Action canceled",
                [StatusBarMessage.RSAPrivateKeyGenerated] = "RSA private key was created",
                [StatusBarMessage.RSAPrivateKeyOpened] = "RSA private key was opened",
                [StatusBarMessage.RSAPrivateKeySaved] = "RSA pivate key was saved",
                [StatusBarMessage.RSAPublicKeyGenerated] = "RSA public key was generated",
                [StatusBarMessage.RSAPublicKeyOpened] = "RSA public key was opened",
                [StatusBarMessage.RSAPublicKeySaved] = "RSA public key was saved"
            };

        public string this[StatusBarMessage message]
        {
            get => msg[message];
        }
    }
}
