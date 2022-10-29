using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotLimited.Framework.Cli.HostBuilder;
using Spectre.Console.Cli;

namespace NotLimited.Framework.Cli.Spectre;

/// <summary>
/// A type registrar to use with Spectre.Console projects.
/// </summary>
public class TypeRegistrar : ITypeRegistrar
{
    private readonly IHostBuilder _builder;
    private readonly CliApp _app;

    /// <summary>
    /// Ctor.
    /// </summary>
    public TypeRegistrar(IHostBuilder builder, CliApp app)
    {
        _builder = builder;
        _app = app;
    }

    /// <inheritdoc />
    public ITypeResolver Build()
    {
        _app.ConfigureInitialAndHostExternal();
        _builder.ConfigureServices(_app.ConfigureServicesExternal);
        var host = _builder.Build();
        _app.ConfigureAppExternal(host.Services);
        return new TypeResolver(host);
    }

    /// <inheritdoc />
    public void Register(Type service, Type implementation)
    {
        _builder.ConfigureServices((_, services) => services.AddSingleton(service, implementation));
    }

    /// <inheritdoc />
    public virtual void RegisterInstance(Type service, object implementation)
    {
        _builder.ConfigureServices((_, services) => services.AddSingleton(service, implementation));
    }

    /// <inheritdoc />
    public void RegisterLazy(Type service, Func<object> func)
    {
        if (func is null)
            throw new ArgumentNullException(nameof(func));

        _builder.ConfigureServices((_, services) => services.AddSingleton(service, _ => func()));
    }
}