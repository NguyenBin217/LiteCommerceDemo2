using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LiteCommerce.Admin
{
    public static class CryptHelper
    {
        public static string Md5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] originalBytes = encoder.GetBytes(text);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            text = BitConverter.ToString(encodedBytes).Replace("-", "");
            var result = text.ToLower();
            return result;
        }
    }
}