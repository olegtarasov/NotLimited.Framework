// using Common.Configuration;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
//
// namespace Common.HostBuilder.Configurators;
//
// /// <summary>
// /// Loads platform configuration depending on environment name and applies environment variable overrides.
// /// </summary>
// public class ConfigurationConfigurator : HostConfiguratorBase
// {
//     /// <inheritdoc />
//     public override void ConfigureHost(IHostBuilder hostBuilder, IConfigurationContext configurationContext)
//     {
//         Logger.Information("Loading common configuration. Environment name: {Environment}",
//                            AssistantConfiguration.GetEnvironmentName());
//
//         hostBuilder.ConfigureAppConfiguration(config => config
//                                                         .AddJsonStream(AssistantConfiguration.GetStream())
//                                                         .AddEnvironmentVariables());
//     }
// }