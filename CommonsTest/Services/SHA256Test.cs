using Commons.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonsTest.Services
{
    [TestClass]
    public class SHA256ServiceTest
    {
        [TestMethod]
        public void SHA256HashingTest()
        {
            //Arrange
            var sha256Service = new SHA256Service();
            var bufferLength = 5000;
            var baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);

            //Act
            var hashedBuffer = sha256Service.GetHash(baseBuffer);
            var verifyResult = sha256Service.VerifyHash(baseBuffer, hashedBuffer);

            //Assert
            Assert.IsTrue(verifyResult);
        }
    }
}
