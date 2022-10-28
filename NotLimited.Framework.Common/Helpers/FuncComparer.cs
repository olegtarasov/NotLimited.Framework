using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// An <see cref="IEqualityComparer{T}"/> implementation that uses lambdas to do comparisons.
/// </summary>
public class FuncComparer<T> : IEqualityComparer<T>
{
    private readonly Func<T?, T?, bool> _equalityFunc;
    private readonly Func<T, int> _hashFunc;

    /// <summary>
    /// Ctor.
    /// </summary>
    public FuncComparer(Func<T?, T?, bool> equalityFunc, Func<T, int> hashFunc)
    {
        _equalityFunc = equalityFunc;
        _hashFunc = hashFunc;
    }

    /// <inheritdoc />
    public bool Equals(T? x, T? y)
    {
        return _equalityFunc(x, y);
    }

    /// <inheritdoc />
    public int GetHashCode([DisallowNull] T obj)
    {
        return _hashFunc(obj);
    }
}