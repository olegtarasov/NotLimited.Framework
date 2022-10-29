using NotLimited.Framework.Common.Progress;

namespace NotLimited.Framework.Cli.Progress;

/// <summary>
/// Implementation that uses Spectre.Console.
/// </summary>
public class SpectreConsoleReporter : IReporter
{
    /// <summary>
    /// Ctor.
    /// </summary>
    public SpectreConsoleReporter(bool externalRefresh = false)
    {
        Progress = new SpectreProgressReporter(externalRefresh);
        Status = new SpectreStatusReporter();
    }

    /// <inheritdoc />
    public IProgressReporter Progress { get; }

    /// <inheritdoc />
    public IStatusReporter Status { get; }
}