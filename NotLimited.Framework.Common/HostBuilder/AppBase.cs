using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotLimited.Framework.Common.HostBuilder.Configurators;
using Serilog;

namespace NotLimited.Framework.Common.HostBuilder;

/// <summary>
/// Base class for app configuration.
/// </summary>
public abstract class AppBase<THost, TConcrete> : IApp<TConcrete>, IConfigurationContext
    where THost : IHost
    where TConcrete : AppBase<THost, TConcrete>, IAppConfiguration
{
    private ILogger? _logger;
    private readonly List<HostConfiguratorBase> _configurators = new();

    /// <summary>
    /// Registered configurators.
    /// </summary>
    protected IReadOnlyList<HostConfiguratorBase> Configurators => _configurators;

    /// <summary>
    /// Ctor.
    /// </summary>
    protected AppBase(Assembly hostAssembly)
    {
        HostAssembly = hostAssembly;

        AddConfigurator(new LoggingConfigurator());
        AddConfigurator(new AutofacConfigurator());
        AddConfigurator(new NodaTimeConfigurator());
    }

    /// <summary>
    /// Logger instance.
    /// </summary>
    /// <remarks>
    /// We are delay-loading the logger because logging is configured in derived constructors.
    /// </remarks>
    protected ILogger Logger => _logger ??= Log.ForContext(GetType());

    /// <inheritdoc />
    public Assembly HostAssembly { get; }

    /// <inheritdoc />
    public TConcrete AddConfigurator(HostConfiguratorBase configurator)
    {
        RemoveConfiguratorIfExists(configurator);
        configurator.OnAddConfigurator(this, this);
        _configurators.Add(configurator);
        return (TConcrete)this;
    }

    IAppConfiguration IAppConfiguration.AddConfiguratorBefore<T>(HostConfiguratorBase configurator)
    {
        return AddConfiguratorBefore<T>(configurator);
    }

    IAppConfiguration IAppConfiguration.AddConfiguratorAfter<T>(HostConfiguratorBase configurator)
    {
        return AddConfiguratorAfter<T>(configurator);
    }

    IAppConfiguration IAppConfiguration.AddConfiguratorFirst(HostConfiguratorBase configurator)
    {
        return AddConfiguratorFirst(configurator);
    }

    IAppConfiguration IAppConfiguration.RemoveConfigurator<T>()
    {
        return RemoveConfigurator<T>();
    }

    IAppConfiguration IAppConfiguration.AddConfigurator(HostConfiguratorBase configurator)
    {
        return AddConfigurator(configurator);
    }

    /// <inheritdoc />
    public TConcrete AddConfiguratorBefore<T>(HostConfiguratorBase configurator) where T : HostConfiguratorBase
    {
        RemoveConfiguratorIfExists(configurator);
        configurator.OnAddConfigurator(this, this);

        var type = typeof(T);
        int idx = _configurators.FindIndex(x => x.GetType() == type);

        if (idx != -1)
            _configurators.Insert(idx, configurator);
        else
            _configurators.Add(configurator);

        return (TConcrete)this;
    }

    /// <inheritdoc />
    public TConcrete AddConfiguratorAfter<T>(HostConfiguratorBase configurator) where T : HostConfiguratorBase
    {
        RemoveConfiguratorIfExists(configurator);
        configurator.OnAddConfigurator(this, this);

        var type = typeof(T);
        int idx = _configurators.FindIndex(x => x.GetType() == type);

        if (idx != -1)
            _configurators.Insert(idx + 1, configurator);
        else
            _configurators.Add(configurator);

        return (TConcrete)this;
    }

    /// <inheritdoc />
    public TConcrete AddConfiguratorFirst(HostConfiguratorBase configurator)
    {
        RemoveConfiguratorIfExists(configurator);
        configurator.OnAddConfigurator(this, this);

        _configurators.Insert(0, configurator);

        return (TConcrete)this;
    }


    /// <inheritdoc />
    public TConcrete RemoveConfigurator<T>() where T : HostConfiguratorBase
    {
        _configurators.RemoveAll(x => x.GetType() == typeof(T));
        return (TConcrete)this;
    }

    /// <inheritdoc />
    public void Run() => RunWithExitCode();

    /// <inheritdoc />
    public abstract int RunWithExitCode();

    /// <summary>
    /// Configure Serilog.
    /// </summary>
    protected void ConfigureSerilog()
    {
        var configuration = new LoggerConfiguration();
        foreach (var configurator in _configurators)
        {
            configurator.ConfigureSerilog(configuration, this);
        }

        Log.Logger = configuration.CreateLogger();
    }

    /// <summary>
    /// Run initial configuration.
    /// </summary>
    protected void ConfigureInitial()
    {
        foreach (var configurator in _configurators)
        {
            configurator.ConfigureInitial(this);
        }
    }

    /// <summary>
    /// Configure the host.
    /// </summary>
    protected void ConfigureHost(IHostBuilder hostBuilder)
    {
        foreach (var configurator in _configurators)
        {
            configurator.ConfigureHost(hostBuilder, this);
        }
    }

    /// <summary>
    /// Configure DI container.
    /// </summary>
    protected void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        foreach (var configurator in _configurators)
        {
            configurator.ConfigureServices(context, services, this);
        }
    }

    /// <summary>
    /// Configure built app.
    /// </summary>
    protected void ConfigureApp(IServiceProvider serviceProvider)
    {
        foreach (var configurator in _configurators)
        {
            configurator.ConfigureApp(serviceProvider, this);
        }
    }

    private void RemoveConfiguratorIfExists(HostConfiguratorBase configurator)
    {
        var type = configurator.GetType();

        // It seems reasonable to support only one configurator of the given type.
        for (int i = _configurators.Count - 1; i >= 0; i--)
        {
            if (_configurators[i].GetType() == type)
                _configurators.RemoveAt(i);
        }
    }
}