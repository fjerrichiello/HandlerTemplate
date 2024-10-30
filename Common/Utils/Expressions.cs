using System.Text.RegularExpressions;

namespace Common.Utils;

public static partial class Expressions
{
    [GeneratedRegex("([a-z0-9])([A-Z])", RegexOptions.Compiled)]
    public static partial Regex SnakeCase();
}