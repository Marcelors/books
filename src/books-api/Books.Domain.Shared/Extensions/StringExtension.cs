using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Books.Domain.Shared.Extensions
{
    public static class StringExtension
    {
        public static bool HasValue(this string value) => !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);

        private static string Chars => "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GenerateString(int length)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(Chars, length)
                              .Select(x => x[random.Next(x.Length)])
                              .ToArray());
        }

        public static string Encrypt(this string value)
        {
            var md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

    }
}
