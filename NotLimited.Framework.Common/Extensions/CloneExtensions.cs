using System.Collections;
using System.Collections.Generic;

namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Extensions that help with object cloning.
/// </summary>
public static class CloneExtensions
{
    /// <summary>
    /// Clones a <see cref="SortedDictionary{TKey,TValue}"/>.
    /// </summary>
    public static SortedDictionary<TKey, TValue> Clone<TKey, TValue>(this SortedDictionary<TKey, TValue> source)
        where TKey : notnull
    {
        return new SortedDictionary<TKey, TValue>(source, source.Comparer);
    }

    /// <summary>
    /// Clones a <see cref="SortedSet{T}"/>.
    /// </summary>
    public static SortedSet<T> Clone<T>(this SortedSet<T> source)
    {
        return new SortedSet<T>(source, source.Comparer);
    }

    /// <summary>
    /// Clones a <see cref="SortedList"/>.
    /// </summary>
    public static SortedList<TKey, TValue> Clone<TKey, TValue>(this SortedList<TKey, TValue> source)
        where TKey : notnull
    {
        return new SortedList<TKey, TValue>(source, source.Comparer);
    }
}