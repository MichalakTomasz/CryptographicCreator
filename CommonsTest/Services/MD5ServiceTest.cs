using Commons.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonsTest.Services
{
    [TestClass]
    public class MD5ServiceTest
    {
        [TestMethod]
        public void Md5HashingTest()
        {
            //Arrange
            var md5Service = new MD5Service();
            var bufferLength = 50;
            var baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);

            //Act
            var hashedBuffer = md5Service.GetMD5Hash(baseBuffer);
            var verifyResult = md5Service.VerifyMD5(baseBuffer, hashedBuffer);

            //Assert
            Assert.IsTrue(verifyResult);
        }
    }
}
