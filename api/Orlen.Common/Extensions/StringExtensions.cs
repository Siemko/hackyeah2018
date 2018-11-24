using System;
using System.Security.Cryptography;
using System.Text;

namespace Orlen.Common.Extensions
{
    public static class StringExtensions
    {
        public static string HashSha256(this string input)
        {
            var algorithm = SHA256.Create();
            var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(input));

            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}