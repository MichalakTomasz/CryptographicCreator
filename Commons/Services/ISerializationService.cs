using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Commons
{
    public interface ISerializationService
    {
        Task<bool> SerializeAsync(byte[] source, string path);
        bool Serialize(RSAParameters rsaParameters, string path);
        Task<byte[]> DeserializeAsync(string path);
        TData Deserialize<TData>(string path) where TData : CryptographicBase;
    }
}