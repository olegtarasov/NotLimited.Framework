using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NotLimited.Framework.Common.HostBuilder.Configurators;

/// <summary>
/// Configures the host to use Autofac instead of built-in .NET DI. 
/// </summary>
public class AutofacConfigurator : HostConfiguratorBase
{
    /// <inheritdoc />
    public override void ConfigureHost(IHostBuilder hostBuilder, IConfigurationContext configurationContext)
    {
        hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}