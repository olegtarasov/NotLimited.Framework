using Microsoft.AspNetCore.Builder;
using NotLimited.Framework.Common.HostBuilder;

namespace NotLimited.Framework.Web.HostBuilder;

/// <summary>
/// Host configurator that can use <see cref="IApplicationBuilder"/>.
/// </summary>
public class WebHostConfiguratorBase : HostConfiguratorBase
{
    /// <summary>
    /// Configures the built app. This overload is used for apps that implement <see cref="IApplicationBuilder"/>.
    /// </summary>
    public virtual void ConfigureApp(IApplicationBuilder builder, IConfigurationContext configurationContext)
    {
    }
}