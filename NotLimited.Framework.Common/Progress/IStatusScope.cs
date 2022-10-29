namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Status reporting scope that doesn't track the number of ticks.
/// </summary>
public interface IStatusScope
{
    /// <summary>
    /// Updates the status.
    /// </summary>
    void SetStatus(string message);
}