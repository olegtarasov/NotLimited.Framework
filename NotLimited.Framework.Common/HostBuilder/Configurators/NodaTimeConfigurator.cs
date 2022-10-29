using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;

namespace NotLimited.Framework.Common.HostBuilder.Configurators;

/// <summary>
/// Configures NodaTime.
/// </summary>
public class NodaTimeConfigurator : HostConfiguratorBase
{
    /// <inheritdoc />
    public override void ConfigureServices(
        HostBuilderContext context,
        IServiceCollection services,
        IConfigurationContext configurationContext)
    {
        services.AddSingleton<IClock>(SystemClock.Instance);
    }
}