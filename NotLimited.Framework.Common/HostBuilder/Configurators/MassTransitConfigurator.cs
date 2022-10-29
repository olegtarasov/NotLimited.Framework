// using System.Reflection;
// using Common.Configuration;
// using MassTransit;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
//
// namespace Common.HostBuilder.Configurators;
//
// /// <summary>
// /// Configures MassTransit.
// /// </summary>
// public class MassTransitConfigurator : HostConfiguratorBase
// {
//     private readonly Assembly _assembly;
//
//     /// <summary>
//     /// Ctor.
//     /// </summary>
//     public MassTransitConfigurator(Assembly assembly)
//     {
//         _assembly = assembly;
//     }
//
//     /// <inheritdoc />
//     public override void ConfigureServices(
//         HostBuilderContext context,
//         IServiceCollection services,
//         IConfigurationContext configurationContext)
//     {
//         Logger.Information("Configuring services for MassTransit");
//
//         var config = context.Configuration.Get<AssistantConfiguration>();
//         services.AddMassTransit(
//             massTransit =>
//             {
//                 massTransit.AddActivities(_assembly);
//                 massTransit.AddConsumers(_assembly);
//                 massTransit.UsingRabbitMq(
//                     (ctx, cfg) =>
//                     {
//                         cfg.Host(config.RabbitMq.Host, "/",
//                                  host =>
//                                  {
//                                      host.Username(config.RabbitMq.User);
//                                      host.Password(config.RabbitMq.Password);
//                                  });
//                         cfg.ConfigureEndpoints(ctx);
//                     });
//             });
//
//         services.AddOptions<MassTransitHostOptions>()
//                 .Configure(options => { options.WaitUntilStarted = true; });
//     }
// }