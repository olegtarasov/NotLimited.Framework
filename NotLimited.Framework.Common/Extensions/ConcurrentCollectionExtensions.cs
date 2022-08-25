using System.Collections.Concurrent;

namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Extensions that help with concurrent collections.
/// </summary>
public static class ConcurrentCollectionExtensions
{
    /// <summary>
    /// Cheks whether a <see cref="ConcurrentBag{T}"/> contains a specified value.
    /// </summary>
    public static bool Contains<T>(this ConcurrentBag<T> bag, T item)
    {
        return bag.TryPeek(out _);
    }

    /// <summary>
    /// Removes specified value from <see cref="ConcurrentBag{T}"/> if it exists.
    /// </summary>
    public static bool Remove<T>(this ConcurrentBag<T> bag, T item)
    {
        return bag.TryTake(out _);
    }

    /// <summary>
    /// Removes a key from <see cref="ConcurrentDictionary{TKey,TValue}"/> if it exists.
    /// </summary>
    public static bool Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
        where TKey : notnull
    {
        return dictionary.TryRemove(key, out _);
    }


    /// <summary>
    /// Adds or updates a value in a <see cref="ConcurrentDictionary{TKey,TValue}"/>.
    /// </summary>
    public static void AddOrUpdate<TKey, TValue>(
        this ConcurrentDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue value) where TKey : notnull
    {
        dictionary.AddOrUpdate(key, value, (_, _) => value);
    }
}