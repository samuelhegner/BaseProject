using System;
using System.Security.Cryptography;
using System.Text;

namespace Helpers
{
    public static class HashingHelper
    {
        public static byte[] ComputeSHA256(string s)
        {
            using SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
        }
    }
}