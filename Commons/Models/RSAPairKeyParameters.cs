using System;
using System.Security.Cryptography;

namespace Commons
{
    [Serializable]
    public class RSAPairKeyParameters
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
