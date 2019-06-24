using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonsTest.Services
{
    [TestClass]
    public class AESSerializationServiceTest
    {
        //Arrange
        AESSerializationService serializationService = new AESSerializationService(
                new GZipCompressionService(),
                new SerializationService(),
                new AESMaskService(new SimpleMaskService()));
        readonly string path = $"{Environment.CurrentDirectory}\\TempSerialization.ser";

        [TestMethod]
        public void AESSerializeDeserializeDataBufferTest()
        {
            //Arrange
            var bufferLength = 2500;
            var buffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                buffer[i] = (byte)random.Next(255);

            //Act
            serializationService.Serialize(buffer, path);
            var deserializedBuffer = serializationService.Deserialize(path);

            //Assert
            CollectionAssert.AreEqual(buffer, deserializedBuffer);
            File.Delete(path);
        }

        [TestMethod]
        public void AESSerializeDeserializeKey()
        {
            //Arrange
            var cryptograpgicService = new AESCryptographicService();
            var aesKey = cryptograpgicService.GenerateKey();

            //Act
            serializationService.SerializeKey(aesKey, path);
            var deserializedKey = serializationService.DeserializeKey(path);
            File.Delete(path);

            //Assert
            CollectionAssert.AreEqual(aesKey.IV, deserializedKey.IV);
            CollectionAssert.AreEqual(aesKey.Key, deserializedKey.Key);
        }
    }
}
