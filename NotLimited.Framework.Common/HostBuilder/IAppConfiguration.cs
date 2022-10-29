namespace NotLimited.Framework.Common.HostBuilder;

/// <summary>
/// App configuration interface.
/// </summary>
public interface IAssistantAppConfiguration<out TConcrete> where TConcrete : IAppConfiguration
{
    /// <summary>
    /// Add a configurator to the app.
    /// </summary>
    /// <param name="configurator">A configurator to add.</param>
    TConcrete AddConfigurator(HostConfiguratorBase configurator);

    /// <summary>
    /// Adds a specified configurator to the position before <typeparamref name="T"/>.
    /// </summary>
    TConcrete AddConfiguratorBefore<T>(HostConfiguratorBase configurator) where T : HostConfiguratorBase;

    /// <summary>
    /// Adds a specified configurator to the position after <typeparamref name="T"/>.
    /// </summary>
    TConcrete AddConfiguratorAfter<T>(HostConfiguratorBase configurator) where T : HostConfiguratorBase;

    /// <summary>
    /// Adds a specified configurator to the beginning of the list. 
    /// </summary>
    TConcrete AddConfiguratorFirst(HostConfiguratorBase configurator);

    /// <summary>
    /// Remove a previously added configurator.
    /// </summary>
    TConcrete RemoveConfigurator<T>() where T : HostConfiguratorBase;
}

/// <summary>
/// App configuration interface.
/// </summary>
public interface IAppConfiguration
{
    /// <summary>
    /// Add a configurator to the app.
    /// </summary>
    /// <param name="configurator">A configurator to add.</param>
    IAppConfiguration AddConfigurator(HostConfiguratorBase configurator);

    /// <summary>
    /// Adds a specified configurator to the position before <typeparamref name="T"/>.
    /// </summary>
    IAppConfiguration AddConfiguratorBefore<T>(HostConfiguratorBase configurator)
        where T : HostConfiguratorBase;

    /// <summary>
    /// Adds a specified configurator to the position after <typeparamref name="T"/>.
    /// </summary>
    IAppConfiguration AddConfiguratorAfter<T>(HostConfiguratorBase configurator)
        where T : HostConfiguratorBase;

    /// <summary>
    /// Adds a specified configurator to the beginning of the list. 
    /// </summary>
    IAppConfiguration AddConfiguratorFirst(HostConfiguratorBase configurator);

    /// <summary>
    /// Remove a previously added configurator.
    /// </summary>
    IAppConfiguration RemoveConfigurator<T>() where T : HostConfiguratorBase;
}