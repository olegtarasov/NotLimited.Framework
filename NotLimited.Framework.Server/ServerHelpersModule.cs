using System;
using System.Configuration;
using System.Web;
using Autofac;
using NotLimited.Framework.Server.Helpers;
using NotLimited.Framework.Server.Services;

namespace NotLimited.Framework.Server
{
	public class ServerHelpersModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
		    bool useAzure = false;
		    string useAzureSetting = ConfigurationManager.AppSettings["UseAzureStorage"];
		    if (!string.IsNullOrEmpty(useAzureSetting) && bool.TryParse(useAzureSetting, out useAzure) && useAzure)
		    {
		        builder.RegisterType<AzureStorageService>()
		               .As<IStorageService>()
		               .SingleInstance();
		    }
		    else
		    {
                builder.RegisterType<FileSystemStorageService>()
                       .As<IStorageService>()
                       .SingleInstance();
		    }

			builder.RegisterType<ImageHelper>()
				.SingleInstance();

			builder.RegisterType<DefaultHostingService>()
					.AsImplementedInterfaces()
					.SingleInstance();
		}
	}
}