namespace Commons
{
    public class AESSerializationService : IAESSerializationService
    {
        #region Fields

        private readonly IGZipCompressionService compressionService;
        private readonly ISerializationService serializationService;
        private readonly IAESMaskService maskService;

        #endregion//Fields

        #region Constructor

        public AESSerializationService(
            IGZipCompressionService compressionService,
            ISerializationService serializationService,
            IAESMaskService aesMaskService)
        {
            this.compressionService = compressionService;
            this.serializationService = serializationService;
            this.maskService = aesMaskService;
        }

        #endregion//Constructor

        #region Mehtods

        public void SerializeKey(AESKey aesKey, string path)
        {
            var masked = maskService.Mask(aesKey);
            serializationService.Serialize(masked, path);
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

        public AESKey DeserializeKey(string path)
        {
            var deserializedBuffer =
                serializationService.Deserialize(path);
            return maskService.Unmask(deserializedBuffer);
        }

        #endregion//Methods
    }
}
