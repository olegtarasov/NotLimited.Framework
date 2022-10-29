namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Generic reporter interface.
/// </summary>
public interface IReporter
{
    /// <summary>
    /// Gets a progress reporter.
    /// </summary>
    IProgressReporter Progress { get; }
    
    /// <summary>
    /// Gets a status reporter.
    /// </summary>
    IStatusReporter Status { get; }
}