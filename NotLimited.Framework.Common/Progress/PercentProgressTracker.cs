namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Tracks progress as a percent of the whole range.
/// </summary>
public class PercentProgressTracker
{
    private readonly int _maxValue;
    private readonly int _reportInterval;

    private int _nextReport;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="maxValue">Maximum value that tracked value can have.</param>
    /// <param name="reportAtPercent">An interval in percent when reports should happen.</param>
    public PercentProgressTracker(int maxValue, int reportAtPercent)
    {
        _maxValue = maxValue;
        _reportInterval = maxValue / 100 * reportAtPercent;
    }

    /// <summary>
    /// Determines whether it's time to report the current value.
    /// </summary>
    /// <param name="value">Current value.</param>
    /// <returns>True if value needs to be reported.</returns>
    public bool ShouldReport(int value)
    {
        if (value == _maxValue)
            return true;
            
        if (value >= _nextReport)
        {
            while (value >= _nextReport)
                _nextReport += _reportInterval;

            return true;
        }

        return false;
    }
}