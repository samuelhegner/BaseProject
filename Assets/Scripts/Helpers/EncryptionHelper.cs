using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Helpers
{
    public static class EncryptionHelper
    {
        private static readonly byte[] IV = new byte[16];
        
        public static string EncryptText(string key, string text, byte[] iv = null)
        {
            byte[] cipherText;

            var initialisationVector = iv ?? IV;
            
            using (Aes aes = Aes.Create())
            {
                aes.Key = HashingHelper.ComputeSHA256(key);
                aes.IV = initialisationVector;
                
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(text);
                        }
 
                        cipherText = memoryStream.ToArray();
                    }
                }
            }
            
            return Convert.ToBase64String(cipherText);
        }

        public static string DecryptText(string key, string cipherText, byte[] iv = null)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);
            
            var initialisationVector = iv ?? IV;
            
            using (Aes aes = Aes.Create())
            {
                aes.Key = HashingHelper.ComputeSHA256(key);
                aes.IV = initialisationVector;
                
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}