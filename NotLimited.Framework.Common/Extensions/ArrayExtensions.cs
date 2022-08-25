using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Array extensios.
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// Copies source array to the beginning of the destination array.
    /// </summary>
    /// <param name="source">Source array.</param>
    /// <param name="destination">Destination array.</param>
    public static void CopyTo(this Array source, Array destination)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (destination == null)
            throw new ArgumentNullException(nameof(destination));
        if (destination.Length < source.Length)
            throw new InvalidOperationException("Target array size must be greater or equal to source array size!");

        Array.Copy(source, 0, destination, 0, source.Length);
    }

    /// <summary>
    /// Computes a hash code of the specified array using <see cref="EqualityComparer{T}.Default"/> for <see cref="object"/>.
    /// </summary>
    /// <param name="array"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int GetArrayHashCode<T>(this T[] array)
    {
        return ((IStructuralEquatable)array).GetHashCode(EqualityComparer<object>.Default);
    }

    /// <summary>
    /// Compares elements of two arrays and returns <code>true</code> if bot array are null or if they have equal
    /// contents.
    /// </summary>
    public static bool EqualsTo<T>(this T[]? a, T[]? b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a == null || b == null)
        {
            return false;
        }

        if (a.Length != b.Length)
        {
            return false;
        }

        // SequenceEqual is quite optimized for arrays, no need to reinvent anything.
        return a.SequenceEqual(b);
    }
}