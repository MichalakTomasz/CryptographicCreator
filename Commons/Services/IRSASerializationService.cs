using System.Security.Cryptography;

namespace Commons
{
    public interface IRSASerializationService
    { 
        void SerializeKey(RSAParameters rsaParameters, string path);
        void Serialize(byte[] buffer, string path);
        RSAParameters DeserializeKey(string path);
        byte[] Deserialize(string path);
    }
}