﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Commons
{
    public class SerializationService : ISerializationService
    {
        #region Public Methods

        public async Task SerializeAsync(ArchiveFrame data, string path)
        {
            try
            {
                if (data != null && Directory.Exists(Path.GetDirectoryName(path)))
                {
                    using (var binaryWriter = 
                        new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                    {
                        await Task.Run(() => 
                        binaryWriter.Write(data.Buffer, 0, data.Buffer.Length));
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Serialization exception: {e.Message}");
            }
        }

        public void Serialize(ArchiveFrame serializedData, string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) &&
                    Directory.Exists(Path.GetDirectoryName(path)) &&
                    !serializedData.Equals(default(ArchiveFrame)))
                {
                    using (var fileStream = File.Open(path, FileMode.OpenOrCreate))
                    {
                        var binaryFormater = new BinaryFormatter();
                        binaryFormater.Serialize(fileStream, serializedData);
                    }
                }
            }

            catch (Exception e)
            {
                Debug.WriteLine($"Serialization exception: {e.Message}");
            }
        }

        public async Task<byte[]> DeserializeArrayBufferAsync(string path)
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

        public byte[] DeserializeArrayBuffer(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && 
                    Directory.Exists(Path.GetDirectoryName(path)))
                {
                    using (var fileStream = File.Open(path, FileMode.Open))
                        return File.ReadAllBytes(path);
                }
                return default(byte[]);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Deserialization exception: {e.Message}");
                return default(byte[]);
            }
        }

        public async Task<ArchiveFrame> DeserializeCompressedDataAsync(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && 
                    Directory.Exists(Path.GetDirectoryName(path)))
                {
                    using (var fileStream = File.Open(path, FileMode.Open))
                    {
                        var binaryFormater = new BinaryFormatter();
                        return await Task.Run(() => 
                        (ArchiveFrame)binaryFormater.Deserialize(fileStream));
                    }    
                }
                return await Task.Run(() => default(ArchiveFrame));
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Deserialization exception: {e.Message}");
                return  default(ArchiveFrame);
            }
        }

        public ArchiveFrame DeserializeCompressedData(string path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && 
                    Directory.Exists(Path.GetDirectoryName(path)))
                {
                    using (var fileStream = File.Open(path, FileMode.Open))
                    {
                        var binaryFormater = new BinaryFormatter();
                        return (ArchiveFrame)binaryFormater.Deserialize(fileStream);
                    } 
                }
                return default(ArchiveFrame);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Deserialization exception: {e.Message}");
                return default(ArchiveFrame);
            }
        }

        #endregion//Public Methods
    }
}
