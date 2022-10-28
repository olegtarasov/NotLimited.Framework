using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Extensions to work with strings.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Removes invalid path characters from a string.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string RemoveInvalidFileNameChars(this string input)
    {
        var chars = Path.GetInvalidFileNameChars();
        if (input.IndexOfAny(chars) == -1)
            return input;

        var sb = new StringBuilder(input);

        for (int i = 0; i < chars.Length; i++)
        {
            sb.Replace(chars[i].ToString(), "");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Concatenates strings putting each one on a new line.
    /// </summary>
    public static string ConcatNewLine(this IEnumerable<string> items)
    {
        var sb = new StringBuilder();
        foreach (var item in items)
        {
            sb.AppendLine(item);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Tries to parse a string as a boolean or as an int, applying truthy logic to the result.
    /// </summary>
    public static bool ParseBoolish(this string input, bool def = false)
    {
        if (bool.TryParse(input, out bool boolResult))
            return boolResult;

        if (int.TryParse(input, out int intResult))
            return intResult != 0;

        return def;
    }

    /// <summary>
    /// Returns at most <paramref name="length"/> items from the start of the string.
    /// </summary>
    public static string Left(this string input, int length)
    {
        return input.Substring(0, length);
    }

    /// <summary>
    /// Returns a substring from start to the first occurence of <paramref name="ch"/>.
    /// </summary>
    public static string Left(this string input, char ch)
    {
        return input.Substring(0, input.IndexOf(ch));
    }

    /// <summary>
    /// Returns a substring starting from <paramref name="start"/> and to the end of the string.
    /// </summary>
    public static string Right(this string input, int start)
    {
        return input.Substring(start);
    }

    /// <summary>
    /// Counts the number of times <paramref name="substring"/> occurs in a string.
    /// </summary>
    public static int CountSubstrings(this string source, string substring)
    {
        int pos = -1;
        int cnt = 0;

        while ((pos = source.IndexOf(substring, pos + 1, StringComparison.Ordinal)) != -1)
            cnt++;

        return cnt;
    }

    /// <summary>
    /// Returns a new string ensuring first charater is lowercase. 
    /// </summary>
    public static string WithLowerFirstChar(this string source)
    {
        if (char.IsLower(source, 0))
            return source;

        return char.ToLowerInvariant(source[0]) + source.Substring(1);
    }

    /// <summary>
    /// Reads a provided string as an array of lines.
    /// </summary>
    public static List<string> ReadLines(this string src)
    {
        var result = new List<string>();

        using (var reader = new StringReader(src))
        {
            while (reader.ReadLine() is { } line)
                result.Add(line);
        }

        return result;
    }

    /// <summary>
    /// Compares two strings ignoring their case with ordinal rules. 
    /// </summary>
    public static bool EqualsIgnoreCase(this string? a, string? b)
    {
        return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Checks whether a string ends with another string ignoring their case with ordinal rules.
    /// </summary>
    public static bool EndsWithIgnoreCase(this string? a, string? b)
    {
        if (a == null || b == null)
            return false;
        return a.EndsWith(b, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Compares two strings optionally ignoring their case with ordinal rules. 
    /// </summary>
    public static bool EqualsOrdinal(this string? a, string? b, bool ignoreCase = false)
    {
        return string.Equals(a, b, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
    }

    /// <summary>
    /// Checks whether a string starts with another string optionally ignoring their case with ordinal rules.
    /// </summary>
    public static bool StartsWithOrdinal(this string? input, string? value, bool ignoreCase = false)
    {
        if (input == null || value == null)
            return false;
        return input.StartsWith(value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
    }

    /// <summary>
    /// Finds a first whitespace character after <paramref name="length"/> and truncates <paramref name="input"/>
    /// at that point. If no whitespace is found, truncates exactly at <paramref name="length"/>.
    /// </summary>
    public static string? TruncateAtWord(this string? input, int length)
    {
        if (input == null || input.Length < length)
            return input;

        int iNextSpace;
        for (iNextSpace = length; iNextSpace < input.Length; iNextSpace++)
        {
            if (char.IsWhiteSpace(input, iNextSpace))
                break;
        }

        if (iNextSpace == input.Length || iNextSpace == 0)
            return $"{input.Substring(0, length).Trim()}";

        return $"{input.Substring(0, iNextSpace).Trim()}";
    }

    /// <summary>
    /// Converts double value to string using <see cref="NumberFormatInfo.InvariantInfo"/>.
    /// </summary>
    public static string ToInvariantString(this double val)
    {
        return val.ToString(NumberFormatInfo.InvariantInfo);
    }
}