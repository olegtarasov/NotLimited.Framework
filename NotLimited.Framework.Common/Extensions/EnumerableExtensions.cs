namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Enumerable extensions.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Flattens an enumerable of enumerables into one single enumerable.
    /// </summary>
    /// <param name="source">Enumerables to flatten.</param>
    public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
    {
        return source.SelectMany(x => x);
    }

    /// <summary>
    /// Returns items in an enumerable that don't equal to specified element.
    /// </summary>
    public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T element)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return source.Where(item => !Equals(item, element));
    }

    /// <summary>
    /// Iterates through an <see cref="IEnumerable{T}"/> and applies an action to each element.
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }

    /// <summary>
    /// Concatenates contents from two <see cref="IEnumerable{T}"/> objects into a new <see cref="HashSet{T}"/>.
    /// </summary>
    /// <remarks>
    /// If <paramref name="source"/> is a <see cref="HashSet{T}"/> itself, its <see cref="HashSet{T}.Comparer"/> is
    /// preserved.
    /// </remarks>
    public static HashSet<T> ConcatHashSet<T>(this IEnumerable<T> source, IEnumerable<T> target)
    {
        var result = CreateHashSetFromSource(source);
        foreach (var item in target)
            result.Add(item);

        return result;
    }

    /// <summary>
    /// Concatenates contents from an <see cref="IEnumerable{T}"/> and params array into a new <see cref="HashSet{T}"/>.
    /// </summary>
    /// <remarks>
    /// If <paramref name="source"/> is a <see cref="HashSet{T}"/> itself, its <see cref="HashSet{T}.Comparer"/> is
    /// preserved.
    /// </remarks>
    public static HashSet<T> ConcatHashSet<T>(this IEnumerable<T> source, params T[] set)
    {
        var result = CreateHashSetFromSource(source);
        foreach (var item in set)
            result.Add(item);

        return result;
    }

    /// <summary>
    /// Returns an empty enumerable if argument is null.
    /// </summary>
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source)
    {
        return source ?? Enumerable.Empty<T>();
    }

    /// <summary>
    /// Checks whether an enumerable is null or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    {
        return source == null || !source.Any();
    }

    private static HashSet<T> CreateHashSetFromSource<T>(IEnumerable<T> source)
    {
        return source is not HashSet<T> sourceSet
                   ? new HashSet<T>(source)
                   : new HashSet<T>(sourceSet, sourceSet.Comparer);
    }
}