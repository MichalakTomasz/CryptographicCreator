using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Commons
{
    public interface ISerializationService
    {
        Task SerializeAsync(byte[] data, string path);
        void Serialize(byte[] data, string path);
        Task<byte[]> DeserializeAsync(string path);
        byte[] Deserialize(string path);
    }
}