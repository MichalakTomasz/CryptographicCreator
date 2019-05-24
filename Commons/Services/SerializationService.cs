using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Commons
{
    public class SerializationService : ISerializationService
    {
        #region Public Methods

        public async Task<bool> SerializeAsync(byte[] source, string path)
        {
            try
            {
                if (source != null && Directory.Exists(Path.GetDirectoryName(path)))
                {
                    using (var binaryWriter = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                    {
                        await Task.Run(() => binaryWriter.Write(source));
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Serialize(RSAParameters rsaParameters, string path)
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(path)))
                {
                    using (var fileStream = File.Open(path, FileMode.OpenOrCreate))
                    {
                        var binaryFormater = new BinaryFormatter();
                        binaryFormater.Serialize(fileStream, rsaParameters);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<byte[]> DeserializeAsync(string path)
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(path)))
                {
                    var fileStream = File.Open(path, FileMode.Open);
                    return await Task.Run(() =>File.ReadAllBytes(path));
                }
                return default(byte[]);
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }

        public TData Deserialize<TData>(string path) where TData : CryptographicBase
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(path)))
                {
                    var fileStream = File.Open(path, FileMode.Open);
                    var binaryFormater = new BinaryFormatter();
                    return (TData)binaryFormater.Deserialize(fileStream);
                }
                return default(TData);
            }
            catch (Exception)
            {
                return default(TData);
            }
        }

        #endregion//Public Methods
    }
}
