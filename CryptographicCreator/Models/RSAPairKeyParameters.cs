using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptographicCreator
{
    class RSAPairKeyParameters
    {
        public RSAPairKeyParameters(RSAParameters publicKeyParameters, RSAParameters privateKeyParameters)
        {
            PublicKeyParameters = publicKeyParameters;
            PrivateKeyParameters = privateKeyParameters;
        }
        public RSAParameters PublicKeyParameters { get; }
        public RSAParameters PrivateKeyParameters { get; }
    }
}
