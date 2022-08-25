using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Collection extensions.
/// </summary>
public static class CollectionExtensions
{
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
    {
        foreach (var item in source)
            collection.Add(item);
    }

    public static List<T> CreateEmptyIfNull<T>(this List<T>? source)
    {
        return source ?? new List<T>();
    }

    public static TValue Max<TSource, TValue>(
        this IEnumerable<TSource> source,
        Func<TSource, double> keySelector,
        Func<TSource, TValue> valueSelector)
    {
        double max = Double.MinValue;
        TValue value = default(TValue);

        foreach (var item in source)
        {
            var key = keySelector(item);
            if (key > max)
            {
                max = key;
                value = valueSelector(item);
            }
        }

        return value;
    }

    public static TValue Max<TSource, TKey, TValue>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TValue> valueSelector) where TKey : IComparable<TKey>
    {
        return SelectByComparison(source, keySelector, valueSelector, 1);
    }

    public static TValue Min<TSource, TKey, TValue>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TValue> valueSelector) where TKey : IComparable<TKey>
    {
        return SelectByComparison(source, keySelector, valueSelector, -1);
    }

    private static TValue SelectByComparison<TSource, TKey, TValue>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TValue> valueSelector,
        int comparisonResult) where TKey : IComparable<TKey>
    {
        if (!source.Any())
        {
            return default(TValue);
        }

        var first = source.First();
        var curKey = keySelector(first);
        var curValue = valueSelector(first);

        foreach (var item in source.Skip(1))
        {
            var key = keySelector(item);
            if (key.CompareTo(curKey) == comparisonResult)
            {
                curKey = key;
                curValue = valueSelector(item);
            }
        }

        return curValue;
    }

    public static IEnumerable<TElement> IterateQueueList<TElement, TContainer>(
        this TContainer root,
        Func<TContainer, IEnumerable<TElement>> elementAccessor,
        Func<TContainer, IEnumerable<TContainer>> childrenAccessor)
    {
        if (root == null)
            throw new ArgumentNullException("root");
        if (elementAccessor == null)
            throw new ArgumentNullException("elementAccessor");
        if (childrenAccessor == null)
            throw new ArgumentNullException("childrenAccessor");

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

    public static IEnumerable<TElement> IterateQueue<TElement, TContainer>(
        this TContainer root,
        Func<TContainer, TElement> elementAccessor,
        Func<TContainer, IEnumerable<TContainer>> childrenAccessor)
    {
        if (root == null)
            throw new ArgumentNullException("root");
        if (elementAccessor == null)
            throw new ArgumentNullException("elementAccessor");
        if (childrenAccessor == null)
            throw new ArgumentNullException("childrenAccessor");

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

    public static IEnumerable<TElement> IterateQueue<TElement, TContainer>(
        this TContainer root,
        Func<TContainer, TElement> elementAccessor,
        Func<TContainer, TContainer> childAccessor) where TContainer : class
    {
        if (root == null)
            throw new ArgumentNullException("root");
        if (elementAccessor == null)
            throw new ArgumentNullException("elementAccessor");
        if (childAccessor == null)
            throw new ArgumentNullException("childAccessor");

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

    public static MinMax<T> MinMax<T>(this IEnumerable<T> en, Func<T, double> selector)
    {
        var result = new MinMax<T>();
        double curItem, curMin, curMax;

        result.Max = result.Min = en.First();
        curMax = curMin = selector(result.Min);

        foreach (var item in en.Skip(1))
        {
            curItem = selector(item);

            if (curItem > curMax)
            {
                result.Max = item;
                curMax = curItem;
            }

            if (curItem < curMin)
            {
                result.Min = item;
                curMin = curItem;
            }
        }

        return result;
    }

    public static MinMax<double> MinMax(this IEnumerable<double> en)
    {
        var result = new MinMax<double> { Max = double.MinValue, Min = double.MaxValue };

        foreach (var item in en)
        {
            if (item > result.Max)
            {
                result.Max = item;
            }

            if (item < result.Min)
            {
                result.Min = item;
            }
        }

        return result;
    }

    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TSource, TKey, TValue>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keyFunc,
        Func<TSource, TValue> valueFunc)
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