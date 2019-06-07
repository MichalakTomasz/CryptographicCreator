using Commons.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonsTest.Services
{
    [TestClass]
    public class SHA512ServiceTest
    {
        [TestMethod]
        public void SHA256HashingTest()
        {
            //Arrange
            var sha512Service = new SHA512Service();
            var bufferLength = 5000;
            var baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);

            //Act
            var hashedBuffer = sha512Service.GetHash(baseBuffer);
            var verifyResult = sha512Service.VerifyHash(baseBuffer, hashedBuffer);

            //Assert
            Assert.IsTrue(verifyResult);
        }
    }
}
