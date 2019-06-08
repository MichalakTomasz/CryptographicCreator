using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonsTest.Services
{
    [TestClass]
    public class SHA256CngServiceTest
    {
        [TestMethod]
        public void SHA256HashingTest()
        {
            //Arrange
            var sha256CngService = new SHA256CngService();
            var bufferLength = 5000;
            var baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);

            //Act
            var hashedBuffer = sha256CngService.GetHash(baseBuffer);
            var verifyResult = sha256CngService.VerifyHash(baseBuffer, hashedBuffer);

            //Assert
            Assert.IsTrue(verifyResult);
        }
    }
}
