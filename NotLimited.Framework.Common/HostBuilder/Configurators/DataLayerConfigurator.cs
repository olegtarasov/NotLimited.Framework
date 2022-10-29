// using Common.Configuration;
// using Common.Contracts.Exceptions;
// using Common.Data;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Serilog;
// using Serilog.Events;
//
// namespace Common.HostBuilder.Configurators;
//
// /// <summary>
// /// Configures data layer for specified context type.
// /// </summary>
// public class DataLayerConfigurator<TContext> : HostConfiguratorBase where TContext : AssistantContextBase
// {
//     private readonly string _schema;
//
//     /// <summary>
//     /// Ctor.
//     /// </summary>
//     public DataLayerConfigurator(string schema)
//     {
//         _schema = schema;
//     }
//
//     /// <inheritdoc />
//     public override void ConfigureSerilog(LoggerConfiguration configuration, IConfigurationContext context)
//     {
//         configuration.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
//     }
//
//     /// <inheritdoc />
//     public override void ConfigureServices(
//         HostBuilderContext context,
//         IServiceCollection services,
//         IConfigurationContext configurationContext)
//     {
//         Logger.Information("Configuring data layer services for context {Context}", typeof(TContext).Name);
//
//         var config = context.Configuration.Get<AssistantConfiguration>();
//         services.AddDbContext<TContext>(options =>
//                                         {
//                                             options.UseAssistantNpgsql(config.Database.ConnectionString, _schema);
//                                         });
//
//         // If there is a context factory, add it as well
//         var factory = typeof(TContext).Assembly
//                                       .GetTypes()
//                                       .FirstOrDefault(x => !x.IsAbstract
//                                                            && x.GetInterfaces()
//                                                                .Any(i => i == typeof(IDbContextFactory<TContext>)));
//         if (factory != null)
//             services.AddSingleton(typeof(IDbContextFactory<TContext>), factory);
//
//         // Add crud repos
//         var types = typeof(TContext).Assembly.GetTypes()
//                                     .Where(x => !x.IsAbstract && x.IsAssignableTo(typeof(ICrudRepositoryMarker)))
//                                     .ToArray();
//
//         foreach (var type in types)
//         {
//             services.AddTransientAsImplementedInterfaces(type, typeof(ICrudRepositoryMarker));
//         }
//
//         // Add self mappings
//         services.AddAutoMapper(cfg => { cfg.AddEntitySelfMapping<TContext>(); });
//     }
//
//     /// <inheritdoc />
//     public override void ConfigureApp(IServiceProvider serviceProvider, IConfigurationContext configurationContext)
//     {
//         Logger.Information("Initializing data layer for context {Context}", typeof(TContext).Name);
//
//         using var scope = serviceProvider.CreateScope();
//
//         if (AssistantConfiguration.GetEnvironmentName().IsLocalDevelopment())
//         {
//             var ctx = scope.ServiceProvider.GetService<TContext>();
//             if (ctx == null)
//             {
//                 throw new ConfigurationException($"{{ContextType}} DB Context was not configured",
//                                                  typeof(TContext).Name);
//             }
//
//             var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
//             if (bool.TryParse(configuration[EnvironmentVariables.DropDb], out bool dropDb)
//                 && dropDb
//                 && ctx.Database.CanConnect())
//             {
//                 Logger.Information("Database drop has been requested. Dropping schema {Schema}", ctx.Schema);
//                 ctx.Database.ExecuteSqlRaw($"DROP SCHEMA IF EXISTS {ctx.Schema} CASCADE;");
//             }
//
//             Logger.Information("Applying migrations to database");
//             ctx.Database.Migrate();
//         }
//     }
// }