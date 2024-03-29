﻿using System.Collections.Concurrent;

namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Collection extensions.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Adds a range of elements to a collection.
    /// </summary>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
    {
        foreach (var item in source)
            collection.Add(item);
    }

    /// <summary>
    /// Iterates elements of a hierarchy depth-first. 
    /// </summary>
    public static IEnumerable<TElement> IterateHierarchy<TElement, TContainer>(
        this TContainer root,
        Func<TContainer, IEnumerable<TElement>> elementAccessor,
        Func<TContainer, IEnumerable<TContainer>> childrenAccessor)
    {
        if (root == null)
            throw new ArgumentNullException(nameof(root));
        if (elementAccessor == null)
            throw new ArgumentNullException(nameof(elementAccessor));
        if (childrenAccessor == null)
            throw new ArgumentNullException(nameof(childrenAccessor));

        var queue = new Queue<TContainer>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();
            foreach (var element in elementAccessor(item))
            {
                yield return element;
            }

            foreach (var child in childrenAccessor(item))
            {
                queue.Enqueue(child);
            }
        }
    }

    /// <summary>
    /// Iterates elements of a hierarchy depth-first. 
    /// </summary>
    public static IEnumerable<TElement> IterateHierarchy<TElement, TContainer>(
        this TContainer root,
        Func<TContainer, TElement> elementAccessor,
        Func<TContainer, IEnumerable<TContainer>> childrenAccessor)
    {
        if (root == null)
            throw new ArgumentNullException(nameof(root));
        if (elementAccessor == null)
            throw new ArgumentNullException(nameof(elementAccessor));
        if (childrenAccessor == null)
            throw new ArgumentNullException(nameof(childrenAccessor));

        var queue = new Queue<TContainer>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            yield return elementAccessor(item);

            foreach (var child in childrenAccessor(item))
            {
                queue.Enqueue(child);
            }
        }
    }

    /// <summary>
    /// Iterates elements of a hierarchy depth-first. 
    /// </summary>
    public static IEnumerable<TElement> IterateHierarchy<TElement, TContainer>(
        this TContainer root,
        Func<TContainer, TElement> elementAccessor,
        Func<TContainer, TContainer?> childAccessor) where TContainer : class
    {
        if (root == null)
            throw new ArgumentNullException(nameof(root));
        if (elementAccessor == null)
            throw new ArgumentNullException(nameof(elementAccessor));
        if (childAccessor == null)
            throw new ArgumentNullException(nameof(childAccessor));

        var queue = new Queue<TContainer>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            yield return elementAccessor(item);

            var child = childAccessor(item);
            if (child != null)
            {
                queue.Enqueue(child);
            }
        }
    }

    /// <summary>
    /// Converts an enumerable to a concurrent dictionary.
    /// </summary>
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TSource, TKey, TValue>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keyFunc,
        Func<TSource, TValue> valueFunc)
        where TKey : notnull
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (keyFunc == null)
            throw new ArgumentNullException(nameof(keyFunc));
        if (valueFunc == null)
            throw new ArgumentNullException(nameof(valueFunc));

        var result = new ConcurrentDictionary<TKey, TValue>();
        foreach (var item in source)
        {
            result.AddOrUpdate(keyFunc(item), valueFunc(item));
        }

        return result;
    }
}