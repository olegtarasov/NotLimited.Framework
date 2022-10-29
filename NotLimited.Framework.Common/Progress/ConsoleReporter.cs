namespace NotLimited.Framework.Common.Progress;

/// <summary>
/// Implementation that uses plain system console.
/// </summary>
public class ConsoleReporter : IReporter
{
    /// <inheritdoc />
    public IProgressReporter Progress { get; } = new ConsoleProgressReporter();

    /// <inheritdoc />
    public IStatusReporter Status { get; } = new ConsoleStatusReporter();
}