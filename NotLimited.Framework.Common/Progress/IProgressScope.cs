namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Progress reporting scope that can track a number of ticks elapsed.
/// </summary>
public interface IProgressScope : IProgressReporter, IDisposable
{
    /// <summary>
    /// Increase the number of elapsed ticks by 1 and report progress.
    /// </summary>
    /// <param name="message">An optional message. Doesn't override previous message if null.</param>
    void Report(string? message = null);

    /// <summary>
    /// Set the number of elapsed ticks to <paramref name="ticks"/> and report progress.
    /// </summary>
    /// <param name="ticks">An absolute number of elapsed ticks.</param>
    /// <param name="message">An optional message. Doesn't override previous message if null.</param>
    void Report(int ticks, string? message = null);
    
    /// <summary>
    /// Gets of sets the maximum number of ticks in a process.
    /// </summary>
    int MaxTicks { get; set; }
    
    /// <summary>
    /// Gets of sets an optional message to be reported with progress.
    /// </summary>
    string? Message { get; set; }
}