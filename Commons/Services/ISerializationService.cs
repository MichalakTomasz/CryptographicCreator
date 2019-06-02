using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Commons
{
    public interface ISerializationService
    {
        Task SerializeAsync(ArchiveFrame data, string path);
        void Serialize(ArchiveFrame data, string path);
        Task<byte[]> DeserializeArrayBufferAsync(string path);
        byte[] DeserializeArrayBuffer(string path);
        Task<ArchiveFrame> DeserializeCompressedDataAsync(string path);
        ArchiveFrame DeserializeCompressedData(string path);
    }
}