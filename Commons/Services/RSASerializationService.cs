using System.Security.Cryptography;

namespace Commons
{
    public class RSASerializationService : IRSASerializationService
    {
        #region Fields

        private readonly IGZipCompressionService compressionService;
        private readonly ISerializationService serializationService;
        private readonly IRSAMaskService rsaMaskService;

        #endregion//Fields

        #region Constructor

        public RSASerializationService(
            IGZipCompressionService compressionService,
            ISerializationService serializationService,
            IRSAMaskService rsaMaskService)
        {
            this.compressionService = compressionService;
            this.serializationService = serializationService;
            this.rsaMaskService = rsaMaskService;
        }

        #endregion//Constructor

        #region Methods

        public void SerializeKey(RSAParameters rsaParameters, string path)
        {
            var maskedKey = rsaMaskService.Mask(rsaParameters);
            serializationService.Serialize(maskedKey, path);
        }

        public void Serialize(byte[] buffer, string path)
        {
            var archiveFrame = compressionService.Compress(buffer);
            serializationService.Serialize(archiveFrame, path);
        }

        public byte[] Deserialize(string path)
        {
            var deserializedBuffer =
                serializationService.Deserialize(path);
            var decompressedData =
                compressionService.Decompress(deserializedBuffer);
            return decompressedData;
        }

        public RSAParameters DeserializeKey(string path)
        {
            var deserializedBuffer =
                serializationService.Deserialize(path);
            var rsaParameters = rsaMaskService.Unmask(deserializedBuffer);
            return rsaParameters;
        }

        #endregion//Methods
    }
}
