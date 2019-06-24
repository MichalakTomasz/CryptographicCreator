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
    public class RSASerializationServiceTest
    {
        //Arrange
        RSASerializationService serializationService = new RSASerializationService(
                new GZipCompressionService(),
                new SerializationService(),
                new RSAMaskService(new SimpleMaskService()));
        readonly string path = $"{Environment.CurrentDirectory}\\TempSerialization.ser";


        [TestMethod]
        public void RSASerializationSeviceTest()
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
        public void RSASerializeDeserializeKey()
        {
            //Arrange
            var cryptograpgicService = new RSACryptographicService();
            var rsaPairKeys = cryptograpgicService.GenerateKeyParameters();
            var privateKeyParameters = rsaPairKeys.PrivateKeyParameters;
            var publicKeyParameters = rsaPairKeys.PublicKeyParameters;

            //Act
            serializationService.SerializeKey(privateKeyParameters, path);
            var deserializedKey = serializationService.DeserializeKey(path);
            File.Delete(path);

            //Assert
            CollectionAssert.AreEqual(privateKeyParameters.D, deserializedKey.D);
            CollectionAssert.AreEqual(privateKeyParameters.DP, deserializedKey.DP);
            CollectionAssert.AreEqual(privateKeyParameters.DQ, deserializedKey.DQ);
            CollectionAssert.AreEqual(privateKeyParameters.Exponent, deserializedKey.Exponent);
            CollectionAssert.AreEqual(privateKeyParameters.InverseQ, deserializedKey.InverseQ);
            CollectionAssert.AreEqual(privateKeyParameters.Modulus, deserializedKey.Modulus);
            CollectionAssert.AreEqual(privateKeyParameters.P, deserializedKey.P);
            CollectionAssert.AreEqual(privateKeyParameters.Q, deserializedKey.Q);

            CollectionAssert.AreEqual(publicKeyParameters.Exponent, deserializedKey.Exponent);
            CollectionAssert.AreEqual(publicKeyParameters.Modulus, deserializedKey.Modulus);
        }
    }
}
