using System.Text.RegularExpressions;

namespace Orlen.Common.Extensions
{
    public static class RegexExtensions
    {
        public static string GetGroupValue(this Match match, string groupName)
        {
            if (match?.Groups == null || match.Groups.Count == 0)
                return null;

            var group = match.Groups[groupName];

            return group?.Value;
        }
    }
}