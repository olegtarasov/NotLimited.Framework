using System.Collections.Generic;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Common.Extensions;

public class PathEqualityComparer : IEqualityComparer<string>
{
    private static readonly PathEqualityComparer _instanse = new PathEqualityComparer();
    public static PathEqualityComparer Instance => _instanse;

    public bool Equals(string x, string y)
    {
        return PathHelpers.PathEquals(x, y);
    }

    public int GetHashCode(string obj)
    {
        return PathHelpers.GetPathHashCode(obj);
    }
}