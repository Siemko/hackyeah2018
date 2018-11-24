using System;
using System.Globalization;

namespace Orlen.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToIsoString(this DateTime date)
        {
            return date.ToString("s", CultureInfo.InvariantCulture);
        }
    }
}
