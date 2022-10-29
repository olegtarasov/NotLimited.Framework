using System.Diagnostics;
using System.Text;

namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Helpers to work with processes
/// </summary>
public static class ProcessHelper
{
    /// <summary>
    /// Runs a process synchronously and captures its output and optionally error stream.
    /// </summary>
    public static string RunAndGetOutput(string fileName, string arguments = "", string workDir = "", bool includeError = false)
    {
        var info = new ProcessStartInfo(fileName, arguments)
                   {
                       RedirectStandardError = includeError,
                       RedirectStandardOutput = true,
                       WorkingDirectory = workDir
                   };
        var process = Process.Start(info) ?? throw new InvalidOperationException("Failed to start the process");
        var builder = new StringBuilder();
        
        process.OutputDataReceived += (sender, args) => builder.AppendLine(args.Data);
        process.BeginOutputReadLine();

        if (includeError)
        {
            process.ErrorDataReceived += (sender, args) => builder.AppendLine(args.Data);
            process.BeginErrorReadLine();
        }

        process.WaitForExit();

        return builder.ToString();
    }
}