using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace NotLimited.Framework.Common.DataAnnotations;

/// <summary>
/// Checks the number of items in an enumerable and enforces the minimum count.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MinEnumerableCount : ValidationAttribute
{
    private const string DefaultError = "'{0}' must have at least {1} elements.";

    /// <summary>
    /// Ctor.
    /// </summary>
    public MinEnumerableCount(int minCount) : base(DefaultError)
    {
        MinCount = minCount;
    }

    /// <summary>
    /// Minimum element count.
    /// </summary>
    public int MinCount { get; }

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        var en = value as IEnumerable;

        if (en == null)
            return false;

        int cnt = 0;
        var enumerator = en.GetEnumerator();
        while (enumerator.MoveNext())
        {
            cnt++;
        }

        return cnt >= MinCount;
    }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return String.Format(ErrorMessageString, name, MinCount);
    }
}