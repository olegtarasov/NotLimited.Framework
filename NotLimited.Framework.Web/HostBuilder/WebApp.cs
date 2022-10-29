using System.Reflection;
using Microsoft.AspNetCore.Builder;
using NotLimited.Framework.Common.HostBuilder;
using NotLimited.Framework.Common.HostBuilder.Configurators;

namespace NotLimited.Framework.Web.HostBuilder;

/// <summary>
/// Web app.
/// </summary>
public class WebApp : AppBase<WebApplication, WebApp>
{
    private readonly WebApplicationBuilder _builder;

    private WebApp(WebApplicationBuilder builder, bool addMvc, Assembly hostAssembly)
        : base(hostAssembly)
    {
        _builder = builder;

        AddConfiguratorAfter<LoggingConfigurator>(new WebApiLoggingSinksConfigurator());
        //AddConfigurator(new WebApiConfigurator(addMvc));
    }

    /// <summary>
    /// Creates a new web app.
    /// </summary>
    public static WebApp Create(string[] args, bool addMvc = false)
    {
        var webAppBuilder = WebApplication.CreateBuilder(args);

        return new WebApp(webAppBuilder, addMvc, Assembly.GetCallingAssembly());
    }

    /// <inheritdoc />
    public override int RunWithExitCode()
    {
        WebApplication host;
        try
        {
            ConfigureSerilog();
            ConfigureInitial();

            ConfigureHost(_builder.Host);

            _builder.Host.ConfigureServices(ConfigureServices);

            host = _builder.Build();

            ConfigureApp(host.Services);
            ConfigureApp(host);
        }
        catch (Exception e)
        {
            Logger.Fatal(e, "Fatal exception while trying to configure the host");
            return 1;
        }

        try
        {
            host.Run();
            return 0;
        }
        catch (Exception e)
        {
            Logger.Fatal(e, "Fatal exception while running the host");
            return 1;
        }
    }

    /// <summary>
    /// Configure built app.
    /// </summary>
    protected void ConfigureApp(IApplicationBuilder builder)
    {
        foreach (var configurator in Configurators.OfType<WebHostConfiguratorBase>())
        {
            configurator.ConfigureApp(builder, this);
        }
    }
}