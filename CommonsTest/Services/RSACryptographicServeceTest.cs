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
    public class RSACryptographicServeceTest
    {
        [TestMethod]
        public void GenerateKeys()
        {
            //Arrange

            var rsaCryptographicService = new RSACryptographicService();

            //Act

            var pairKeys = rsaCryptographicService.GenerateKeyParameters();
            var privateKey = pairKeys.PrivateKeyParameters;
            var publicKey = pairKeys.PublicKeyParameters;

            //Assert

            Assert.IsNotNull(pairKeys);
            Assert.IsNotNull(privateKey);
            Assert.IsNotNull(publicKey);
        }

        [TestMethod]
        public void EncryptDecryptTest()
        {
            //Arrange
            var text = "This is simple Example text to testing.";
            var byteArrayWithText = Encoding.Default.GetBytes(text);
            var rsaCryptographicService = new RSACryptographicService();
            var pairKeys = rsaCryptographicService.GenerateKeyParameters();
            var publicKey = pairKeys.PublicKeyParameters;
            var privateKey = pairKeys.PrivateKeyParameters;

            //Act
            var encryptedArray1 = rsaCryptographicService.Encrypt(byteArrayWithText, publicKey);
            var encryptedArray2 = rsaCryptographicService.Encrypt(byteArrayWithText, privateKey);
            var encryptedArray3 = rsaCryptographicService.Encrypt(text, Encoding.Default, publicKey);
            var encryptedArray4 = rsaCryptographicService.Encrypt(text, Encoding.Default, privateKey);
            var decryptedArray1 = rsaCryptographicService.Decrypt(encryptedArray1, privateKey);
            var decryptedArray2 = rsaCryptographicService.Decrypt(encryptedArray2, privateKey);
            var decryptedArray3 = rsaCryptographicService.Decrypt(encryptedArray3, privateKey);
            var decryptedArray4 = rsaCryptographicService.Decrypt(encryptedArray4, privateKey);
            var dectyptedText1 = Encoding.Default.GetString(decryptedArray1);
            var decryptedText2 = Encoding.Default.GetString(decryptedArray2);
            var decryptedText3 = Encoding.Default.GetString(decryptedArray3);
            var decryptedText4 = Encoding.Default.GetString(decryptedArray4);

            //Assert
            Assert.AreEqual(text, dectyptedText1);
            Assert.AreEqual(text, decryptedText2);
            Assert.AreEqual(text, decryptedText3);
            Assert.AreEqual(text, decryptedText4);
        }
    }
}
