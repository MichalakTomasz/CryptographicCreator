using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonsTest.Services
{
    [TestClass]
    public class GZipCompressionServiceTest
    {
        [TestMethod]
        public void GZipCompressDecompressTest()
        {
            //Arrange
            GZipCompressionService compressionService = new GZipCompressionService();

            var bufferLength = 50000;
            var buffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                buffer[i] = (byte)random.Next(255);

            //Act
            var compressedBuffer = compressionService.Compress(buffer);
            var decompressedBuffer = compressionService.Decompress(compressedBuffer);

            //Assert
            CollectionAssert.AreEqual(buffer, decompressedBuffer);
        }
    }
}
