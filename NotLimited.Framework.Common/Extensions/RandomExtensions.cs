namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Extensions to work with <see cref="Random"/>.
/// </summary>
public static class RandomExtensions
{
    /// <summary>
    /// Gets next double with specified high bound.
    /// </summary>
    public static double NextDouble(this Random rnd, double high)
    {
        return rnd.NextDouble() * high;
    }

    /// <summary>
    /// Gets next double with specified high and low bounds.
    /// </summary>
    public static double NextDouble(this Random rnd, double low, double high)
    {
        return rnd.NextDouble() * (high - low) + low;
    }
}