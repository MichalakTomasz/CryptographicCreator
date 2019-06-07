using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Commons
{
    public interface ISerializationService
    {
        Task SerializeAsync(BufferFrame data, string path);
        void Serialize(BufferFrame data, string path);
        Task<byte[]> DeserializeArrayBufferAsync(string path);
        byte[] DeserializeArrayBuffer(string path);
        Task<BufferFrame> DeserializeCompressedDataAsync(string path);
        BufferFrame DeserializeCompressedData(string path);
    }
}