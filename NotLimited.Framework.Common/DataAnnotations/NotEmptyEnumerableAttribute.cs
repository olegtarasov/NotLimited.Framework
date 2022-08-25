using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace NotLimited.Framework.Common.DataAnnotations;

/// <summary>
/// Checks whether enumerable contains at least one element.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class NotEmptyEnumerableAttribute : ValidationAttribute
{
    private const string DefaultError = "'{0}' must have at least one element.";

    /// <summary>
    /// Ctor.
    /// </summary>
    public NotEmptyEnumerableAttribute() : base(DefaultError)
    {
    }

    /// <inheritdoc />
    public override bool IsValid(object? value)
    {
        var en = value as IEnumerable;
        return (en != null && en.GetEnumerator().MoveNext());
    }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return String.Format(ErrorMessageString, name);
    }
}