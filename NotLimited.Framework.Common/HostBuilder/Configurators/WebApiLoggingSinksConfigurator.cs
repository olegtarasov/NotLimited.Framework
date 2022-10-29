using NotLimited.Framework.Common.Helpers;
using Serilog;

namespace NotLimited.Framework.Common.HostBuilder.Configurators;

/// <summary>
/// Configures logging for Web API hosts.
/// </summary>
public class WebApiLoggingSinksConfigurator : HostConfiguratorBase
{
    /// <inheritdoc />
    public override void ConfigureSerilog(LoggerConfiguration configuration, IConfigurationContext context)
    {
        configuration.AddFileSink(context.HostAssembly);
        configuration.AddSystemConsoleSink(true);
    }
}