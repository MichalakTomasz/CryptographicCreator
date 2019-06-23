using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonsTest.Services
{
    [TestClass]
    public class AESSimpleMaskServiceTest
    {
        [TestMethod]
        public void AESSimpleMaskServiceMaskUnmaskTest()
        {
            //Assert
            AESCryptographicService aescryptographicService = new AESCryptographicService();
            var aesKey = aescryptographicService.GenerateKey();
            AESMaskService aesMaskkService = new AESMaskService(new SimpleMaskService());
            var bufferLength = 150000;
            var baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);

            //Act
            var masked = aesMaskkService.Mask(aesKey);
            var unmasked = aesMaskkService.Unmask(masked);

            //Arrange
            CollectionAssert.AreEqual(aesKey.IV, unmasked.IV);
            CollectionAssert.AreEqual(aesKey.Key, unmasked.Key);
        }
    }
}
