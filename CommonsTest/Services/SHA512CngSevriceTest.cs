using Commons.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonsTest.Services
{
    [TestClass]
    public class SHA512CngServiceTest
    {
        [TestMethod]
        public void SHA512CngHashingTest()
        {
            //Arrange
            var sha512CngService = new SHA512CngService();
            var bufferLength = 5000;
            var baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);

            //Act
            var hashedBuffer = sha512CngService.GetHash(baseBuffer);
            var verifyResult = sha512CngService.VerifyHash(baseBuffer, hashedBuffer);

            //Assert
            Assert.IsTrue(verifyResult);
        }
    }
}
