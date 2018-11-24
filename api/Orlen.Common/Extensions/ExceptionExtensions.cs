using System;

namespace Orlen.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception ex)
        {
            var result = string.Empty;

            var currentEx = ex;

            while (currentEx != null)
            {
                result += currentEx.Message + Environment.NewLine;
                currentEx = currentEx.InnerException;
            }

            return result;
        }
    }
}