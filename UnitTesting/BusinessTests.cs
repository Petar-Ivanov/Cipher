using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Cipher;
using Business;

namespace UnitTesting
{
    [TestFixture]
    public static class BusinessTests
    {
        [TestCase]
        public static void LatinSymbolsAreEncrypted()
        {
            AES aes = new AES();
            string text = "Test string";
            string encryptedText = aes.Encrypt(text);
            Assert.AreNotEqual(text,encryptedText);
        }
        [TestCase]
        public static void NumbersAreEncrypted()
        {
            AES aes = new AES();
            string text = "3218553";
            string encryptedText = aes.Encrypt(text);
            Assert.AreNotEqual(text, encryptedText);
        }
        [TestCase]
        public static void CyrillicSymbolsAreEncrypted()
        {
            AES aes = new AES();
            string text = "Тест";
            string encryptedText = aes.Encrypt(text);
            Assert.AreNotEqual(text, encryptedText);
        }
        [TestCase]
        public static void LatinSymbolsAreDecrypted()
        {
            AES aes = new AES();
            string text = "Test";
            string encryptedText = aes.Encrypt(text);
            string decryptedText = aes.Decrypt(encryptedText);
            Assert.AreEqual(text,decryptedText);
        }
        [TestCase]
        public static void NumbersAreDecrypted()
        {
            AES aes = new AES();
            string text = "15646545";
            string encryptedText = aes.Encrypt(text);
            string decryptedText = aes.Decrypt(encryptedText);
            Assert.AreEqual(text, decryptedText);
        }
        [TestCase]
        public static void CyrillicSymbolsAreDecrypted()
        {
            AES aes = new AES();
            string text = "Тест";
            string encryptedText = aes.Encrypt(text);
            string decryptedText = aes.Decrypt(encryptedText);
            Assert.AreEqual(text, decryptedText);
        }
        [TestCase]
        public static void WeakPasswordIsEvaluated()
        {
            string password = "g1f";
            Assert.IsTrue(PasswordManager.Evaluate(password)<50);
        }
        [TestCase]
        public static void AveragePasswordIsEvaluated()
        {
            string password = "g1fF597";
            Assert.IsTrue(PasswordManager.Evaluate(password) >50 && PasswordManager.Evaluate(password)<75);
        }
        [TestCase]
        public static void StrongPasswordIsEvaluated()
        {
            string password = "g1fF597%*Hh_83";
            Assert.IsTrue(PasswordManager.Evaluate(password) >= 75);
        }
        [TestCase]
        public static void GenerateStrongPassword()
        {
            Assert.IsTrue(PasswordManager.Evaluate(PasswordManager.GeneratePassword("&$^8~`",12)) == 100);
        }
        [TestCase]
        public static void GenerateShortPassword()
        {
            Assert.IsTrue(PasswordManager.GeneratePassword(" ",3).Length==3);
        }
        [TestCase]
        public static void GenerateValidPassword()
        {
            string forbidden = ")(&^~`dfV23";
            bool containsForbidden = false;
            string password = PasswordManager.GeneratePassword(forbidden, 20);
            foreach (var chr in forbidden)
            {
                if (password.Contains(chr)) containsForbidden = true;
            }
            Assert.IsFalse(containsForbidden);
        }
    }
}
