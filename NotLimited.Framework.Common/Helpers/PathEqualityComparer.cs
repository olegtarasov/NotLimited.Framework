using System.Collections.Generic;

namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Equality comparer that compares paths.
/// </summary>
public class PathEqualityComparer : IEqualityComparer<string>
{
    /// <summary>
    /// Comparer static instance.
    /// </summary>
    public static readonly PathEqualityComparer Instance = new();

    /// <inheritdoc />
    public bool Equals(string? x, string? y)
    {
        if (x == null && y == null)
            return true;

        if (x == null || y == null)
            return false;

        return PathHelpers.PathEquals(x, y);
    }

    /// <inheritdoc />
    public int GetHashCode(string obj)
    {
        return PathHelpers.GetPathHashCode(obj);
    }
}