using System.Reflection;
using Microsoft.Extensions.Hosting;
using NotLimited.Framework.Cli.HostBuilder.Configurators;
using NotLimited.Framework.Cli.Spectre;
using NotLimited.Framework.Common.HostBuilder;
using NotLimited.Framework.Common.HostBuilder.Configurators;
using Spectre.Console.Cli;

namespace NotLimited.Framework.Cli.HostBuilder;

/// <summary>
/// Command line app.
/// </summary>
public class CliApp : ExternallyControlledApp<IHost, CliApp>
{
    private readonly CommandApp _app;
    private readonly string[] _args;

    /// <summary>
    /// Ctor.
    /// </summary>
    protected CliApp(string[] args, Assembly hostAssembly) : base(
        Host.CreateDefaultBuilder(args), hostAssembly)
    {
        _args = args;

        AddConfiguratorAfter<LoggingConfigurator>(new CliLoggingSinksConfigurator());

        // ReSharper disable once VirtualMemberCallInConstructor
        _app = new CommandApp(CreateTypeRegistrar(HostBuilder));
    }

    /// <summary>
    /// Creates a new CLI app.
    /// </summary>
    public static CliApp Create(string[] args)
    {
        return new CliApp(args, Assembly.GetCallingAssembly());
    }

    /// <summary>
    /// Configure Spectre.Cli.
    /// </summary>
    public CliApp WithCliConfiguration(Action<IConfigurator> config)
    {
        _app.Configure(config);
        return this;
    }

    /// <summary>
    /// Set the default command for Spectre.Cli.
    /// </summary>
    public CliApp WithDefaultCommand<TCommand>() where TCommand : class, ICommand
    {
        _app.SetDefaultCommand<TCommand>();
        return this;
    }

    /// <inheritdoc />
    public override int RunWithExitCode()
    {
        ConfigureSerilog();
        return _app.Run(_args);
    }

    /// <summary>
    /// Creates a type registrar.
    /// </summary>
    protected virtual TypeRegistrar CreateTypeRegistrar(IHostBuilder hostBuilder) =>
        new TypeRegistrar(hostBuilder, this);
}