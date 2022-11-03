using System.Runtime.CompilerServices;

namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Creates a progress reporting scope and uses System.Console as output.
/// </summary>
public class ConsoleProgressScope : ConsoleProgressReporter, IProgressScope
{
    private readonly PercentProgressTracker? _tracker = null;

    private int _curTicks = 0;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="maxTicks">Maximum number of ticks in a process</param>
    /// <param name="message">Optional message to be reported with progress</param>
    /// <param name="reportAtPercent">If specified, reports only at discrete percent intervals, not every tracked event.</param>
    public ConsoleProgressScope(int maxTicks, string? message, int? reportAtPercent = null)
    {
        MaxTicks = maxTicks;
        Message = message;
        if (reportAtPercent != null)
        {
            _tracker = new(maxTicks, reportAtPercent.Value);
        }
    }

    /// <inheritdoc />
    public void Report(string? message = null)
    {
        Report(_curTicks + 1, message);
    }

    /// <inheritdoc />
    public void Report(int ticks, string? message = null)
    {
        _curTicks = ticks;
        if (_tracker != null)
        {
            if (_tracker.ShouldReport(_curTicks))
                ReportInternal(_curTicks, message);
        }
        else
        {
            ReportInternal(_curTicks, message);
        }
    }

    /// <inheritdoc />
    public int MaxTicks { get; set; }

    /// <inheritdoc />
    public string? Message { get; set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ReportInternal(int ticks, string? message = null)
    {
        Console.WriteLine($"[ {ticks} / {MaxTicks} ] {message ?? Message ?? string.Empty}");
    }

    /// <inheritdoc />
    public void Dispose()
    {
    }
}