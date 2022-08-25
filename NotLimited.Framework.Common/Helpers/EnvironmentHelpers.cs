using System.Diagnostics;

namespace NotLimited.Framework.Common.Helpers;

public static class EnvironmentHelpers
{
    public static void ShellOpen(string fileName)
    {
        Process.Start(new ProcessStartInfo(fileName) {UseShellExecute = true});
    }
}