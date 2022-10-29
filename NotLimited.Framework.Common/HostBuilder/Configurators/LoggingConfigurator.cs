using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace NotLimited.Framework.Common.HostBuilder.Configurators;

/// <summary>
/// Configures logging.
/// </summary>
/// <remarks>
/// Initial Serilog configuration is done in <see cref="AppBase{THost,TConcrete}"/> derivatives,
/// since logger config is specific to app type. 
/// </remarks>
public class LoggingConfigurator : HostConfiguratorBase
{
    private const LogEventLevel DefaultLevel = LogEventLevel.Debug;

    /// <inheritdoc />
    public override void ConfigureSerilog(LoggerConfiguration configuration, IConfigurationContext context)
    {
        var level = DefaultLevel;
        string? logLevel = Environment.GetEnvironmentVariable("LOGLEVEL");
        if (!string.IsNullOrEmpty(logLevel))
        {
            if (!Enum.TryParse(logLevel, true, out level))
                level = DefaultLevel;
        }

        configuration.MinimumLevel.Is(level)
                     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning);

        configuration.Enrich.FromLogContext()
                     .Enrich.WithExceptionDetails();
    }

    /// <inheritdoc />
    public override void ConfigureHost(IHostBuilder hostBuilder, IConfigurationContext configurationContext)
    {
        hostBuilder.UseSerilog();
        Logger.Information("Configured logging");
    }
}