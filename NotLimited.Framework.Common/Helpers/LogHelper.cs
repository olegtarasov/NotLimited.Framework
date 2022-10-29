using System.Reflection;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Logging helpers.
/// </summary>
public static class LogHelper
{
    /// <summary>
    /// File logging template.
    /// </summary>
    public const string FileTemplate =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}][{SourceContext}] {Message:lj}{NewLine}{Exception}";

    /// <summary>
    /// Console logging template.
    /// </summary>
    public const string ConsoleTemplate = "{Message:lj}{NewLine}{Exception}";

    // /// <summary>
    // /// Logs an exception. When there is a <see cref="StructuredException"/> in exception hierarchy, logs it
    // /// with structure.
    // /// </summary>
    // public static void LogException(this ILogger logger, Exception exception)
    // {
    //     var structuredException = ExtractStructuredException(exception);
    //     if (structuredException != null)
    //     {
    //         logger.Error(exception, structuredException.MessageTemplate, structuredException.Args);
    //     }
    //     else
    //     {
    //         logger.Error(exception, exception.Message);
    //     }
    // }

    /// <summary>
    /// Adds console logging sink.
    /// </summary>
    public static LoggerConfiguration AddSystemConsoleSink(this LoggerConfiguration config, bool colored)
    {
        return config.WriteTo.Console(
            theme: colored ? SystemConsoleTheme.Colored : ConsoleTheme.None,
            outputTemplate: ConsoleTemplate);
    }

    /// <summary>
    /// Adds file logging sink deriving file name from specified assembly.
    /// </summary>
    public static LoggerConfiguration AddFileSink(this LoggerConfiguration config, Assembly hostAssembly)
    {
        string? assemblyName = hostAssembly.GetName().Name;
        return AddFileSink(config, Path.Combine("logs", $"{assemblyName ?? "log"}.txt"));
    }

    /// <summary>
    /// Adds file logging sink using specified file name.
    /// </summary>
    public static LoggerConfiguration AddFileSink(this LoggerConfiguration config, string fileName)
    {
        return config.WriteTo.File(
            fileName,
            outputTemplate: FileTemplate,
            fileSizeLimitBytes: 1024 * 1024 * 5,
            retainedFileCountLimit: 10,
            rollOnFileSizeLimit: true);
    }

    // private static StructuredException? ExtractStructuredException(Exception exception)
    // {
    //     var cur = exception;
    //     while (cur != null)
    //     {
    //         if (cur is StructuredException structuredException)
    //             return structuredException;
    //
    //         cur = cur.InnerException;
    //     }
    //
    //     return null;
    // }
}