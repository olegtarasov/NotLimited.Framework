using System;
using System.Web;
using Autofac;
using NotLimited.Framework.Server.Helpers;

namespace NotLimited.Framework.Server
{
	public class ServerHelpersModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<FileSystemHelper>()
				.SingleInstance();

			builder.RegisterType<ImageHelper>()
				.SingleInstance();
		}
	}
}