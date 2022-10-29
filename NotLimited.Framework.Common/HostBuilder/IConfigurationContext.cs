using System.Reflection;

namespace NotLimited.Framework.Common.HostBuilder;

/// <summary>
/// Host configuration context.
/// </summary>
public interface IConfigurationContext
{
    /// <summary>
    /// An assembly that created the <see cref="AppBase{THost,TConcrete}"/> instance. 
    /// </summary>
    Assembly HostAssembly { get; }
}