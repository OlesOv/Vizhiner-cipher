using NUnit.Framework;
using System;
using Vizhiner_cipher;

namespace VizhinerCipherTests
{
    [TestFixture]
    public class CipherTests
    {
        Cipher _cipher;
        [SetUp]
        public void SetUp()
        {
            _cipher = new Cipher();
        }
        [TestCase("Карл у Клары украл кораллы", "кларнет", "Хлрь б Пюкьы дшхтц цобнрюё", Alphabets.Russian)]
        [TestCase("Карл у Клары украл кораллы", "а", "Карл у Клары украл кораллы", Alphabets.Russian)]

        [TestCase("Just testing something", "a", "Just testing something", Alphabets.English)]
        [TestCase("Just testing something", "test", "Cykm mikmbry lhqwmamfz", Alphabets.English)]
        public void Cipher_Encrypt(string originalText, string key, string expectedEncryptedText, string alphabet)
        {
            string encryptedText = _cipher.Encrypt(originalText, key, alphabet);
            
            Assert.AreEqual(expectedEncryptedText, encryptedText);
        }

        [TestCase("Хлрь б Пюкьы дшхтц цобнрюё", "кларнет", "Карл у Клары украл кораллы", Alphabets.Russian)]
        [TestCase("Карл у Клары украл кораллы", "а", "Карл у Клары украл кораллы", Alphabets.Russian)]

        [TestCase("Just testing something", "a", "Just testing something", Alphabets.English)]
        [TestCase("Cykm mikmbry lhqwmamfz", "test", "Just testing something", Alphabets.English)]
        public void Cipher_Decrypt(string originalText, string key, string expectedDecryptedText, string alphabet)
        {
            string decryptedText = _cipher.Decrypt(originalText, key, alphabet);

            Assert.AreEqual(expectedDecryptedText, decryptedText);
        }
    }
}