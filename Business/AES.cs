using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business
{
    public class AES
    {
        AesCryptoServiceProvider crypt_provider;
     
        public byte[] Key
        {
            get { return crypt_provider.Key; }
            set { crypt_provider.Key = value; }
        }
        public byte[] IV
        {
            get { return crypt_provider.IV; }
            set { crypt_provider.IV = value; }
        }
        public AES()
        {
            crypt_provider = new AesCryptoServiceProvider();
            crypt_provider.BlockSize = 128;
            crypt_provider.KeySize = 256;
            crypt_provider.GenerateIV();
            crypt_provider.GenerateKey();
            crypt_provider.Mode = CipherMode.CBC;
            crypt_provider.Padding = PaddingMode.PKCS7;
        }

        public string Encrypt(string text)
        {
            text = EncodeNonAsciiCharacters(text);
            ICryptoTransform transform = crypt_provider.CreateEncryptor();
            byte[] encrypted_bites = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(text), 0, text.Length);
            string output = Convert.ToBase64String(encrypted_bites);
            return output;
        }

        public string Decrypt(string cipher_text)
        {
            ICryptoTransform transform = crypt_provider.CreateDecryptor();
            byte[] enc_bytes = Convert.FromBase64String(cipher_text);
            byte[] dec_bytes = transform.TransformFinalBlock(enc_bytes, 0, enc_bytes.Length);
            string output = ASCIIEncoding.ASCII.GetString(dec_bytes);
            output = DecodeEncodedNonAsciiCharacters(output);
            return output;
        }
        private static string EncodeNonAsciiCharacters(string value)
        {
            return Regex.Replace(
              value,
              @"[^\x00-\x7F]",
              m => String.Format("\\u{0:X4}", (int)m.Value[0]));
        }
        private string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }
    }
}
