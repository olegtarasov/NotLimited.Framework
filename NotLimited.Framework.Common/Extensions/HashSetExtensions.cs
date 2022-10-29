namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// HashSet extensions.
/// </summary>
public static class HashSetExtensions
{
    /// <summary>
    /// Compares two hashsets using <see cref="HashSet{T}.SetEquals"/>, handling the case when any of them can be null.
    /// </summary>
    public static bool HashSetEquals<T>(this HashSet<T>? source, HashSet<T>? compareTo)
    {
        if (source == null && compareTo == null)
        {
            return true;
        }

        if (ReferenceEquals(source, compareTo))
        {
            return true;
        }

        if (source == null || compareTo == null)
        {
            return false;
        }

        return source.SetEquals(compareTo);
    }
}