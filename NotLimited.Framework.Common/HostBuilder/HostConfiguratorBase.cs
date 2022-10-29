using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace NotLimited.Framework.Common.HostBuilder;

/// <summary>
/// Base class for host configuration/
/// </summary>
public abstract class HostConfiguratorBase
{
    private ILogger? _logger;

    /// <summary>
    /// Logger.
    /// </summary>
    protected ILogger Logger => _logger ??= Log.ForContext(GetType());

    /// <summary>
    /// Ctor.
    /// </summary>
    protected HostConfiguratorBase()
    {
    }

    /// <summary>
    /// Called before this configurator is added to the app configurators list. This is a good place to add
    /// extra ("child") configurators.
    /// </summary>
    public virtual void OnAddConfigurator(IAppConfiguration app, IConfigurationContext configurationContext)
    {
    }

    /// <summary>
    /// Called before any host configuration. This is a good place to configure generic things like JsonSerializerOptions
    /// or some such.
    /// </summary>
    public virtual void ConfigureInitial(IConfigurationContext configurationContext)
    {
    }

    /// <summary>
    /// Configures Serilog.
    /// </summary>
    public virtual void ConfigureSerilog(LoggerConfiguration configuration, IConfigurationContext context)
    {
    }

    /// <summary>
    /// Configures the host. This is a good place to load configuration/
    /// </summary>
    public virtual void ConfigureHost(IHostBuilder hostBuilder, IConfigurationContext configurationContext)
    {
    }

    /// <summary>
    /// Configures dependency injection.
    /// </summary>
    public virtual void ConfigureServices(
        HostBuilderContext context,
        IServiceCollection services,
        IConfigurationContext configurationContext)
    {
    }

    /// <summary>
    /// Configures the built app. This overload is used for apps that don't implement IApplicationBuilder.
    /// </summary>
    public virtual void ConfigureApp(IServiceProvider serviceProvider, IConfigurationContext configurationContext)
    {
    }
}