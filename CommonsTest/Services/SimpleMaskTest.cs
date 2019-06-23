using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonsTest.Services
{
    [TestClass]
    public class SimpleMaskTest
    {
        [TestMethod]
        public void SipmleMaskServiceUnmaskTest()
        {
            //Arrange
            SimpleMaskService simpleMask = new SimpleMaskService();
            var bufferLength = 150000;
            var baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);

            //Act
            var masked = simpleMask.MaskUnmask(baseBuffer);
            var unmasked = simpleMask.MaskUnmask(masked);

            //Assert
            CollectionAssert.AreEqual(baseBuffer, unmasked);
        }
    }
}
