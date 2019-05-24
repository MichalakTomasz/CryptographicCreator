using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographicCreator.Models
{
    public class RSAData
    {
        public string PrivateAndPublicKey { get; set; }
        public string PublicKey { get; set; }
        public string Text { get; set; }
        public string EncryptedText { get; set; }
        public int BitsKey { get; set; }
    }
}
