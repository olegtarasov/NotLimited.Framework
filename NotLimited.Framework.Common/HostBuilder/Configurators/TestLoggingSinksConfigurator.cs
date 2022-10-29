using NotLimited.Framework.Common.Helpers;
using Serilog;

namespace NotLimited.Framework.Common.HostBuilder.Configurators;

/// <summary>
/// Configures logging sinks for test hosts.
/// </summary>
public class TestLoggingSinksConfigurator : HostConfiguratorBase
{
    /// <inheritdoc />
    public override void ConfigureSerilog(LoggerConfiguration configuration, IConfigurationContext context)
    {
        configuration.AddSystemConsoleSink(false);
    }
}