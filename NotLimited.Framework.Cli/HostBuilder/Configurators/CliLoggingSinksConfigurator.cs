using NotLimited.Framework.Cli.Helpers;
using NotLimited.Framework.Common.HostBuilder;
using Serilog;

namespace NotLimited.Framework.Cli.HostBuilder.Configurators;

/// <summary>
/// Configures logging for Spectre.Console hosts.
/// </summary>
public class CliLoggingSinksConfigurator : HostConfiguratorBase
{
    /// <inheritdoc />
    public override void ConfigureSerilog(LoggerConfiguration configuration, IConfigurationContext context)
    {
        configuration.AddSpectreConsoleSink();
    }
}