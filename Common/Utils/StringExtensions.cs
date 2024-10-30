using System.Text.RegularExpressions;

namespace Common.Utils;

public static class StringExtensions
{
    public static string ToSnakeCase(this string input) => Expressions.SnakeCase().Replace(input, "$1_$2");
}