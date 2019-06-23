using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Commons
{
    public class SerializationService : ISerializationService
    {
        #region Public Methods

        public async Task SerializeAsync(byte[] data, string path)
        {
            try
            {
                if (data != null && Directory.Exists(Path.GetDirectoryName(path)))
                {
                    using (var binaryWriter = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                    {
                        await Task.Run(() => binaryWriter.Write(data, 0, data.Length));
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Serialization exception: {e.Message}");
            }
        }

        public void Serialize(byte[] buffer, string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) &&
                    Directory.Exists(Path.GetDirectoryName(path)) &&
                    !buffer.Equals(default(byte[])))
                {
                    using (var fileStream = File.Open(path, FileMode.OpenOrCreate))
                    using (var binaryWriter = new BinaryWriter(fileStream))
                    {
                        binaryWriter.Write(buffer.Length);
                        binaryWriter.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Serialization exception: {e.Message}");
            }
        }

        public async Task<byte[]> DeserializeAsync(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && 
                    Directory.Exists(Path.GetDirectoryName(path)))
                {
                    using (var fileStream = File.Open(path, FileMode.Open))
                        return await Task.Run(() => File.ReadAllBytes(path));
                }
                return default(byte[]);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Deserialization exception: {e.Message}");
                return default(byte[]);
            }
        }

        public byte[] Deserialize(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && 
                    Directory.Exists(Path.GetDirectoryName(path)))
                {
                    using (var fileStream = File.Open(path, FileMode.Open))
                    using (var binaryReader = new BinaryReader(fileStream))
                    {
                        var bufferLength = binaryReader.ReadInt32();
                        var resultBuffer = new byte[bufferLength];
                        binaryReader.Read(resultBuffer, 0, bufferLength);
                        return resultBuffer;
                    }
                }
                return default(byte[]);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Deserialization exception: {e.Message}");
                return default(byte[]);
            }
        }

        #endregion//Public Methods
    }
}
