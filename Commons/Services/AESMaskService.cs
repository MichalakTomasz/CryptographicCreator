using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Commons
{
    public class AESMaskService : IAESMaskService
    {
        private readonly ISimpleMaskService simpleMaskService;

        public AESMaskService(ISimpleMaskService simpleMaskService)
            => this.simpleMaskService = simpleMaskService;

        public byte[] Mask(AESKey aesKey)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, aesKey);
                    var aesBuffer = memoryStream.ToArray();
                    var masked = simpleMaskService.MaskUnmask(aesBuffer);
                    return masked;
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($"AESMaskService exception: {e.Message}");
                return default(byte[]);
            }
        }

        public AESKey Unmask(byte[] buffer)
        {
            try
            {
                var unmasked = simpleMaskService.MaskUnmask(buffer);
                using (var memoryStream = new MemoryStream(unmasked))
                {
                    var binaryFormatter = new BinaryFormatter();
                    return (AESKey)binaryFormatter.Deserialize(memoryStream);
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($"AESMaskService exception: {e.Message}");
                return default(AESKey);
            }
        }
    }
}
