using System;

namespace Orlen.Common.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToBase64(this byte[] value)
        {
            return Convert.ToBase64String(value);
        }
    }
}