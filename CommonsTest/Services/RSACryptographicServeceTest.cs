using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace CommonsTest.Services
{
    [TestClass]
    public class RSACryptographicServeceTest
    {
        [TestMethod]
        public void RSACompareKeyModulesTest()
        {
            //Arrange

            var rsaCryptographicService = new RSACryptographicService();
            var rsaPairKeyParameters1 = rsaCryptographicService.GenerateKeyParameters();
            var rsaPairKeyParameters2 = rsaCryptographicService.GenerateKeyParameters();

            //Act

            var isFromTheSameBase1 = rsaCryptographicService.CompareKeyBases(
                rsaPairKeyParameters1.PrivateKeyParameters, 
                rsaPairKeyParameters1.PrivateKeyParameters);

            var isFromTheSameBase2 = rsaCryptographicService.CompareKeyBases(
                rsaPairKeyParameters1.PublicKeyParameters,
                rsaPairKeyParameters2.PublicKeyParameters);

            var isFromTheSameBase3 = rsaCryptographicService.CompareKeyBases(
                rsaPairKeyParameters2.PrivateKeyParameters,
                rsaPairKeyParameters2.PublicKeyParameters);

            //Assert

            Assert.IsTrue(isFromTheSameBase1);
            Assert.IsFalse(isFromTheSameBase2);
            Assert.IsTrue(isFromTheSameBase3);
        }

        [TestMethod]
        public void RSAGenerateKeys()
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
        public void RSAEncryptDecryptTest()
        {
            //Arrange
            var text = "This is simple Example text to testing.";
            var byteArrayFromText = Encoding.Default.GetBytes(text);
            int bufferLength = 40;
            byte[] baseBuffer = new byte[bufferLength];
            var random = new Random();
            for (int i = 0; i < bufferLength; i++)
                baseBuffer[i] = (byte)random.Next(255);
            var rsaCryptographicService = new RSACryptographicService();
            var pairKeys = rsaCryptographicService.GenerateKeyParameters();
            var publicKey = pairKeys.PublicKeyParameters;
            var privateKey = pairKeys.PrivateKeyParameters;

            //Act
            var encryptedArray1 = rsaCryptographicService.Encrypt(byteArrayFromText, publicKey);
            var encryptedArray2 = rsaCryptographicService.Encrypt(byteArrayFromText, privateKey);
            var encryptedArray3 = rsaCryptographicService.Encrypt(text, Encoding.Default, publicKey);
            var encryptedArray4 = rsaCryptographicService.Encrypt(text, Encoding.Default, privateKey);
            var encryptedArray5 = rsaCryptographicService.Encrypt(baseBuffer, privateKey);
            var encryptedArray6 = rsaCryptographicService.Encrypt(baseBuffer, publicKey);
            var decryptedArray1 = rsaCryptographicService.Decrypt(encryptedArray1, privateKey);
            var decryptedArray2 = rsaCryptographicService.Decrypt(encryptedArray2, privateKey);
            var decryptedArray3 = rsaCryptographicService.Decrypt(encryptedArray3, privateKey);
            var decryptedArray4 = rsaCryptographicService.Decrypt(encryptedArray4, privateKey);
            var decryptedArray5 = rsaCryptographicService.Decrypt(encryptedArray5, privateKey);
            var decryptedArray6 = rsaCryptographicService.Decrypt(encryptedArray6, privateKey);
            var dectyptedText1 = Encoding.Default.GetString(decryptedArray1);
            var decryptedText2 = Encoding.Default.GetString(decryptedArray2);
            var decryptedText3 = Encoding.Default.GetString(decryptedArray3);
            var decryptedText4 = Encoding.Default.GetString(decryptedArray4);

            //Assert
            Assert.AreEqual(text, dectyptedText1);
            Assert.AreEqual(text, decryptedText2);
            Assert.AreEqual(text, decryptedText3);
            Assert.AreEqual(text, decryptedText4);
            Assert.AreEqual(text, decryptedText3);
            Assert.AreEqual(text, decryptedText4);
            CollectionAssert.AreEqual(baseBuffer, decryptedArray5);
            CollectionAssert.AreEqual(baseBuffer, decryptedArray6);
        }
    }
}
