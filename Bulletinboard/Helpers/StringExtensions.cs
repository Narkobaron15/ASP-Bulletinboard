using System.Text.RegularExpressions;

namespace Bulletinboard.Helpers;

    public static partial class StringExtensions
    {
        [GeneratedRegex("(?<!^)(?=[A-Z])")] private static partial Regex MyRegex();
        public static string[] SplitCamelCase(this string source) => MyRegex().Split(source);
        public static string SplitCamelCaseIntoStr(this string source) => string.Join(' ', SplitCamelCase(source));
        public static string FirstChars(this string? source)
    {
        return string.IsNullOrWhiteSpace(source)
            ? string.Empty
            : source.Length > 25 ? source[..22] + "..." : source;
    }
}
