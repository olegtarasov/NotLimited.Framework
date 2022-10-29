using System.Reflection;
using Microsoft.Extensions.Hosting;
using NotLimited.Framework.Common.HostBuilder.Configurators;

namespace NotLimited.Framework.Common.HostBuilder;

/// <summary>
/// Unit test app.
/// </summary>
public class TestApp : ExternallyControlledApp<IHost, TestApp>
{
    private TestApp(IHostBuilder hostBuilder, Assembly hostAssembly) : base(hostBuilder, hostAssembly)
    {
        AddConfiguratorAfter<LoggingConfigurator>(new TestLoggingSinksConfigurator());
    }

    /// <summary>
    /// Creates a new app instance.
    /// </summary>
    public static TestApp Create(IHostBuilder builder) => new(builder, Assembly.GetCallingAssembly());

    /// <inheritdoc />
    public override int RunWithExitCode()
    {
        throw new NotSupportedException();
    }
}