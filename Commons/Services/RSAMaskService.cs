using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Commons
{
    public class RSAMaskService : IRSAMaskService
    {
        private readonly ISimpleMaskService simpleMaskService;

        public RSAMaskService(ISimpleMaskService simpleMaskService)
        {
            this.simpleMaskService = simpleMaskService;
        }

        public byte[] Mask(RSAParameters rsaKey)
        {
            using (var memoryStream = new MemoryStream())
            {
                var serializableRSAParameters = new SerializableRSAParameters
                {
                    Exponent = rsaKey.Exponent,
                    Modulus = rsaKey.Modulus,
                    P = rsaKey.P,
                    Q = rsaKey.Q,
                    DP = rsaKey.DP,
                    DQ = rsaKey.DQ,
                    InverseQ = rsaKey.InverseQ,
                    D = rsaKey.D
                };
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, serializableRSAParameters);
                var masked = simpleMaskService.MaskUnmask(memoryStream.ToArray());
                return masked;
            }
        }

        public RSAParameters Unmask(byte[] buffer)
        {
            var unmasked = simpleMaskService.MaskUnmask(buffer);
            using (var memoryStream = new MemoryStream(unmasked))
            {
                var binaryFormatter = new BinaryFormatter();
                var serializableRSAParameters = (SerializableRSAParameters)binaryFormatter.Deserialize(memoryStream);
                return new RSAParameters
                {
                    Exponent = serializableRSAParameters.Exponent,
                    Modulus = serializableRSAParameters.Modulus,
                    P = serializableRSAParameters.P,
                    Q = serializableRSAParameters.Q,
                    DP = serializableRSAParameters.DP,
                    DQ = serializableRSAParameters.DQ,
                    InverseQ = serializableRSAParameters.InverseQ,
                    D = serializableRSAParameters.D
                };
            }
        }

        [Serializable]
        class SerializableRSAParameters
        {
            public byte[] Exponent { get; set; }
            public byte[] Modulus { get; set; }
            public byte[] P { get; set; }
            public byte[] Q { get; set; }
            public byte[] DP { get; set; }
            public byte[] DQ { get; set; }
            public byte[] InverseQ { get; set; }
            public byte[] D { get; set; }
        }
    }
}
