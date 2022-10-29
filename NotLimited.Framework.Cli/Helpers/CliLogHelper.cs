using NotLimited.Framework.Common.Helpers;
using Serilog;
using Serilog.Sinks.Spectre;

namespace NotLimited.Framework.Cli.Helpers;

/// <summary>
/// Cli logging helpers.
/// </summary>
public static class CliLogHelper
{
    /// <summary>
    /// Adds Spectre.Console logging sink.
    /// </summary>
    public static LoggerConfiguration AddSpectreConsoleSink(this LoggerConfiguration config)
    {
        return config.WriteTo.Spectre(LogHelper.ConsoleTemplate);
    }
}