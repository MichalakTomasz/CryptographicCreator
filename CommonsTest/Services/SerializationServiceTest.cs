using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonsTest.Services
{
    [TestClass]
    public class SerializationServiceTest
    {
        [TestMethod]
        public void SerializeDeserializeTest()
        {
            //Arrange

            var bufferLength = 500;
            var buffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                buffer[i] = (byte)random.Next(255);
            var path = $"{Environment.CurrentDirectory}\\TempSerialization.ser";
            var serializationService = new SerializationService();
            var serializationData = new BufferFrame
            {
                Buffer = buffer,
                OriginalBufferLength = buffer.Length
            };

            //Act

            serializationService.Serialize(serializationData, path);
            var deserializedBuffer = serializationService.DeserializeArrayBuffer(path);

            //Assert

           // CollectionAssert.AreEqual(buffer, deserializedBuffer);
           // File.Delete(path);
        }
    }
}
