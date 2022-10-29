// using System.Reflection;
// using Common.Api;
// using Common.Api.OpenApi;
// using Common.Configuration;
// using Common.Contracts.Constants;
// using Common.Contracts.Exceptions;
// using Common.Contracts.Helpers;
// using Common.Contracts.Models;
// using MicroElements.Swashbuckle.NodaTime;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.HttpLogging;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.OpenApi.Models;
// using OpenTelemetry.Resources;
// using OpenTelemetry.Trace;
// using Serilog;
// using Serilog.Events;
//
// namespace Common.HostBuilder.Configurators;
//
// /// <summary>
// /// Configures all things Web API and optionally MVC.
// /// </summary>
// public class WebApiConfigurator : HostConfiguratorBase
// {
//     private readonly bool _addMvc;
//
//     /// <summary>
//     /// Ctor.
//     /// </summary>
//     public WebApiConfigurator(bool addMvc)
//     {
//         _addMvc = addMvc;
//     }
//
//     /// <inheritdoc />
//     public override void ConfigureSerilog(LoggerConfiguration configuration, IConfigurationContext context)
//     {
//         configuration.MinimumLevel.Override("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware",
//                                             LogEventLevel.Information);
//     }
//
//     /// <inheritdoc />
//     public override void ConfigureServices(
//         HostBuilderContext context,
//         IServiceCollection services,
//         IConfigurationContext configurationContext)
//     {
//         Logger.Information("Configuring services for WebApi");
//
//         services.PostConfigure<ApiBehaviorOptions>(
//             options =>
//             {
//                 options.InvalidModelStateResponseFactory =
//                     ctx =>
//                     {
//                         var problemDetails = new ValidationProblemDetails(ctx.ModelState);
//                         return new ObjectResult(new ApiError(ErrorCode.RequestError,
//                                                              "One or more validation errors occurred.",
//                                                              new Dictionary<string, string[]>(
//                                                                  problemDetails.Errors)))
//                                {
//                                    StatusCode = StatusCodes.Status400BadRequest
//                                };
//                     };
//             });
//
//         if (_addMvc)
//         {
//             services.AddControllersWithViews();
//         }
//         else
//         {
//             services.AddControllers();
//         }
//
//         if (!AssistantConfiguration.GetEnvironmentName().IsProduction())
//         {
//             services.AddHttpLogging(logging => logging.LoggingFields = HttpLoggingFields.All);
//         }
//
//         services.AddEndpointsApiExplorer();
//         services.AddSwaggerGen(options =>
//                                {
//                                    options.ConfigureForNodaTimeWithSystemTextJson(
//                                        JsonHelper.DefaultOptions, shouldGenerateExamples: false);
//                                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                                                                            {
//                                                                                In = ParameterLocation.Header,
//                                                                                Description = "Auth token",
//                                                                                Name = "Authorization",
//                                                                                Type = SecuritySchemeType.Http,
//                                                                                Scheme = "bearer"
//                                                                            });
//                                    options.OperationFilter<AuthOperationFilter>();
//                                    var xmlFiles = Directory.EnumerateFiles(
//                                        AppContext.BaseDirectory,
//                                        "*.xml",
//                                        SearchOption.TopDirectoryOnly);
//                                    foreach (string filename in xmlFiles)
//                                    {
//                                        options.IncludeXmlComments(
//                                            Path.Combine(AppContext.BaseDirectory, filename));
//                                    }
//                                });
//
//         var config = context.Configuration.Get<AssistantConfiguration>();
//
//         const string platformName = "AssistantPlatform";
//         string projectName = string.Join(".", platformName, Assembly.GetEntryAssembly()?.GetName().Name);
//         var environment = AssistantConfiguration.GetEnvironmentName();
//
//         services.AddOpenTelemetryTracing(builder =>
//                                          {
//                                              builder.AddOtlpExporter(options =>
//                                                                      {
//                                                                          options.Endpoint =
//                                                                              new Uri(config.Observability.OtlpEndpoint);
//                                                                      })
//                                                     .SetResourceBuilder(
//                                                         ResourceBuilder.CreateDefault()
//                                                                        .AddService(projectName, platformName))
//                                                     .AddSource(Tracing.ActivitySourceName)
//                                                     .AddHttpClientInstrumentation(options =>
//                                                     {
//                                                         options.Enrich =
//                                                             (activity, _, _) =>
//                                                             {
//                                                                 activity.AddTag(
//                                                                     "environment", environment
//                                                                 );
//                                                             };
//                                                     })
//                                                     .AddAspNetCoreInstrumentation(options =>
//                                                     {
//                                                         options.Enrich = (activity, _, _) =>
//                                                                          {
//                                                                              activity.AddTag(
//                                                                                  "environment", environment
//                                                                              );
//                                                                          };
//                                                     })
//                                                     .AddEntityFrameworkCoreInstrumentation(options =>
//                                                     {
//                                                         options.SetDbStatementForText = true;
//                                                     })
//                                                     .AddMassTransitInstrumentation();
//                                          });
//     }
//
//     /// <inheritdoc />
//     public override void ConfigureApp(IApplicationBuilder builder, IConfigurationContext configurationContext)
//     {
//         Logger.Information("Configuring app for WebApi");
//
//         if (builder is not WebApplication app)
//             throw new SystemStructuredException("Expected builder to be a WebApplication");
//
//         if (!AssistantConfiguration.GetEnvironmentName().IsProduction())
//         {
//             app.UseHttpLogging();
//         }
//
//         app.UseMiddleware<ErrorHandlingMiddleware>();
//
//         app.UseSwagger();
//         app.UseSwaggerUI();
//
//         if (_addMvc)
//         {
//             app.UseStaticFiles();
//         }
//
//         //app.UseHttpsRedirection();
//
//         app.UseAuthorization();
//
//         app.MapControllers();
//     }
// }