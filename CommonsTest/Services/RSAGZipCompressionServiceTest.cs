using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonsTest.Services
{
    [TestClass]
    public class RSAGZipCompressionServiceTest
    {
        [TestMethod]
        public void CompressDecompressTest()
        {
            //Arrange

            var arrayLength = 300;
            var patternArray = new byte[arrayLength];
            var gzip = new RSAGZipCompressionService();
            var random = new Random();
            for (var i = 0; i < arrayLength; i++)
            {
                patternArray[i] = (byte)random.Next(255);
            }
            var rsaService = new RSACryptographicService();
            var rsaPairParameters = rsaService.GenerateKeyParameters();
            var privateKey = rsaPairParameters.PrivateKeyParameters;
            var publicKey = rsaPairParameters.PublicKeyParameters;

            //Act

            var compressedArrayFrame = gzip.Compress(patternArray);
            byte[] decompressedArray;
            decompressedArray = gzip.DecompressByteBuffer(compressedArrayFrame);

            var compressedPrivateKey = gzip.Compress(privateKey);
            var compressePublicKey = gzip.Compress(publicKey);
            RSAParameters decompressedPrivateKey;
            decompressedPrivateKey = gzip.DecompressRSAParameters(compressedPrivateKey);
            RSAParameters decompressedPublicKey;
            decompressedPublicKey = gzip.DecompressRSAParameters(compressePublicKey);


            //Assert

            CollectionAssert.AreEqual(patternArray, decompressedArray);

            CollectionAssert.AreEqual(privateKey.D, decompressedPrivateKey.D);
            CollectionAssert.AreEqual(privateKey.DP, decompressedPrivateKey.DP);
            CollectionAssert.AreEqual(privateKey.DQ, decompressedPrivateKey.DQ);
            CollectionAssert.AreEqual(privateKey.Exponent, decompressedPrivateKey.Exponent);
            CollectionAssert.AreEqual(privateKey.InverseQ, decompressedPrivateKey.InverseQ);
            CollectionAssert.AreEqual(privateKey.Modulus, decompressedPrivateKey.Modulus);
            CollectionAssert.AreEqual(privateKey.P, decompressedPrivateKey.P);
            CollectionAssert.AreEqual(privateKey.Q, decompressedPrivateKey.Q);

            CollectionAssert.AreEqual(publicKey.D, decompressedPublicKey.D);
            CollectionAssert.AreEqual(publicKey.DP, decompressedPublicKey.DP);
            CollectionAssert.AreEqual(publicKey.DQ, decompressedPublicKey.DQ);
            CollectionAssert.AreEqual(publicKey.Exponent, decompressedPublicKey.Exponent);
            CollectionAssert.AreEqual(publicKey.InverseQ, decompressedPublicKey.InverseQ);
            CollectionAssert.AreEqual(publicKey.Modulus, decompressedPublicKey.Modulus);
            CollectionAssert.AreEqual(publicKey.P, decompressedPublicKey.P);
            CollectionAssert.AreEqual(publicKey.Q, decompressedPublicKey.Q);
        }
    }
}
