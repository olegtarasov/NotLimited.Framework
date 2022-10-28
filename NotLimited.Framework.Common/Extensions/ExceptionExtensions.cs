using System;
using System.Text;

namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Helpers to work with exceptions.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Iterates the <see cref="Exception.InnerException"/> hierarchy and combines exception names and messages into
    /// a string.
    /// </summary>
    public static string GetMessageHierarchy(this Exception exception)
    {
        var builder = new StringBuilder();
        int cnt = 0;
        foreach (var ex in exception.IterateHierarchy(x => x, x => x.InnerException))
        {
            builder.Append(' ', cnt * 2).Append(ex.GetType().Name).Append(": ").AppendLine(ex.Message);
        }

        return builder.ToString();
    }
}