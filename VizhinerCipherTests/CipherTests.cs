using NUnit.Framework;
using System;
using Vizhiner_cipher;

namespace VizhinerCipherTests
{
    public class CipherTests
    {


        [TestCase("���� � ����� ����� �������", "�������", "���� � ����� ����� �������", Alphabets.Russian)]
        [TestCase("���� � ����� ����� �������", "�", "���� � ����� ����� �������", Alphabets.Russian)]

        [TestCase("Just testing something", "a", "Just testing something", Alphabets.English)]
        [TestCase("Just testing something", "test", "Cykm mikmbry lhqwmamfz", Alphabets.English)]
        public void EncryptingTest(string originalText, string key, string expectedEncryptedText, string alphabet)
        {
            //Arrange

            //Act
            string encryptedText = Cipher.Encrypt(originalText, key, alphabet);
            
            //Assert
            Assert.AreEqual(expectedEncryptedText, encryptedText);
        }

        [TestCase("���� � ����� ����� �������", "�������", "���� � ����� ����� �������", Alphabets.Russian)]
        [TestCase("���� � ����� ����� �������", "�", "���� � ����� ����� �������", Alphabets.Russian)]

        [TestCase("Just testing something", "a", "Just testing something", Alphabets.English)]
        [TestCase("Cykm mikmbry lhqwmamfz", "test", "Just testing something", Alphabets.English)]
        public void DecryptingTest(string originalText, string key, string expectedDecryptedText, string alphabet)
        {
            //Arrange

            //Act
            string decryptedText = Cipher.Decrypt(originalText, key, alphabet);

            //Assert
            Assert.AreEqual(expectedDecryptedText, decryptedText);
        }
    }
}