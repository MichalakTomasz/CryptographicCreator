using System.Security.Cryptography;

namespace Commons
{
    public class RSASerializationService : IRSASerializationService
    {
        #region Fields

        private readonly ICompressionService compressionService;
        private readonly ISerializationService serializationService;

        #endregion//Fields

        #region Constructor

        public RSASerializationService(
            ICompressionService compressionService,
            ISerializationService serializationService)
        {
            this.compressionService = compressionService;
            this.serializationService = serializationService;
        }

        #endregion//Constructor

        #region Methods

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
            BufferFrame deserializedBuffer =
                serializationService.DeserializeCompressedData(path);
            byte[] decompressedData =
                compressionService.DecompressByteBuffer(deserializedBuffer);
            return decompressedData;
        }

        public RSAParameters DeserializeKey(string path)
        {
            BufferFrame deserializedBuffer =
                serializationService.DeserializeCompressedData(path);
            RSAParameters rsaParameters =
                compressionService.DecompressRSAParameters(deserializedBuffer);
            return rsaParameters;
        }

        #endregion//Methods
    }
}
