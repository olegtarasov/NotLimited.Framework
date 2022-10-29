using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NotLimited.Framework.Common.HostBuilder;

/// <summary>
/// An application whose configuration flow is controlled externally (like Spectre.Cli or XUnit integration).
/// </summary>
public abstract class ExternallyControlledApp<THost, TConcrete> : AppBase<THost, TConcrete>
    where THost : IHost
    where TConcrete : AppBase<THost, TConcrete>, IAppConfiguration
{
    /// <summary>
    /// Host builder instance.
    /// </summary>
    protected readonly IHostBuilder HostBuilder;

    /// <summary>
    /// Ctor.
    /// </summary>
    protected ExternallyControlledApp(IHostBuilder hostBuilder, Assembly hostAssembly) : base(hostAssembly)
    {
        HostBuilder = hostBuilder;
    }

    /// <summary>
    /// Runs Serilog configuration.
    /// </summary>
    public void ConfigureSerilogExternal()
    {
        ConfigureSerilog();
    }

    /// <summary>
    /// Runs initial and host configuration.
    /// </summary>
    public void ConfigureInitialAndHostExternal()
    {
        ConfigureInitial();
        ConfigureHost(HostBuilder);
    }

    /// <summary>
    /// Runs DI configuration.
    /// </summary>
    public void ConfigureServicesExternal(HostBuilderContext context, IServiceCollection services) =>
        ConfigureServices(context, services);

    /// <summary>
    /// Runs app configuration.
    /// </summary>
    public void ConfigureAppExternal(IServiceProvider serviceProvider) => ConfigureApp(serviceProvider);
}