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
    public class MD5ServiceTest
    {
        [TestMethod]
        void Md5HashingTest()
        {
            //Arrange
            var md5Service = new MD5Service();
            var bufferLength = 1000;
            var baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);

            //Act
            var hashedBuffer = md5Service.GetMD5Hash(baseBuffer);

            //Assert
            Assert.AreEqual(baseBuffer, hashedBuffer);
        }
    }
}
