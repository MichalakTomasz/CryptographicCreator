using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonsTest.Services
{
    [TestClass]
    public class AESCryptographicServiceTest
    {
        //Arrange
        AESCryptographicService aesCryptographicService = new AESCryptographicService();
        int bufferLength = 2500;
        byte[] baseBuffer;
        AESKey aesKey;
        
        public AESCryptographicServiceTest()
        {
            baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);
            aesKey = aesCryptographicService.GenerateKey();
        }
        
        [TestMethod]
        public void AESEncryptDecryptByteBufferTest()
        {
            //Act
            var encryptedData = aesCryptographicService.Encrypt(baseBuffer, aesKey);
            var decryptedData = aesCryptographicService.Decrypt(encryptedData, aesKey);

            //Assert 
            CollectionAssert.AreEqual(baseBuffer, decryptedData);
        }
    }
}
