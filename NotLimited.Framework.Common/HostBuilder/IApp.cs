namespace NotLimited.Framework.Common.HostBuilder;

/// <summary>
/// App interface.
/// </summary>
public interface IApp<out TConcrete> : IAssistantAppConfiguration<TConcrete>, IAppConfiguration
    where TConcrete : IAssistantAppConfiguration<TConcrete>, IAppConfiguration
{
    /// <summary>
    /// Run the app synchronously.
    /// </summary>
    void Run();

    /// <summary>
    /// Run the app with exit code.
    /// </summary>
    int RunWithExitCode();
}