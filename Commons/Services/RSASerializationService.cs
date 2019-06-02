using System.Security.Cryptography;

namespace Commons
{
    public class RSASerializationService : IRSASerializationService
    {
        private readonly IRSACryptographicService rsaCryptographicService;
        private readonly ICompressionService compressionService;
        private readonly ISerializationService serializationService;

        public RSASerializationService(
            IRSACryptographicService rsaCryptographicService,
            ICompressionService compressionService,
            ISerializationService serializationService)
        {
            this.rsaCryptographicService = rsaCryptographicService;
            this.compressionService = compressionService;
            this.serializationService = serializationService;
        }

        public void SerializeKey(RSAParameters rsaParameters, string path)
        {
            var compressedKey = compressionService.Compress(rsaParameters);
            serializationService.Serialize(compressedKey, path);
        }

        public void SerializeEncryptedData(byte[] buffer, string path)
        {
            var archiveFrame = compressionService.Compress(buffer);
            serializationService.SerializeAsync(archiveFrame, path);
        }

        public byte[] DeserializeEncryptedData(string path)
        {
            ArchiveFrame deserializedBuffer = 
                serializationService.DeserializeCompressedData(path);
            byte[] decompressedData = 
                compressionService.DecompressByteBuffer(deserializedBuffer);
            return decompressedData;
        }

        public RSAParameters DeserializeKey(string path)
        {
            ArchiveFrame deserializedBuffer = 
                serializationService.DeserializeCompressedData(path);
            RSAParameters rsaParameters = 
                compressionService.DecompressRSAParameters(deserializedBuffer);
            return rsaParameters;
        }
    }
}
