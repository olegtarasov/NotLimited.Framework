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
			string path = HttpContext.Current.Server.MapPath("~/");
			if (string.IsNullOrEmpty(path))
				throw new InvalidOperationException("Can't get server root!");

			builder.RegisterType<FileSystemHelper>()
				.WithParameter("serverRoot", path)
				.SingleInstance();

			builder.RegisterType<ImageHelper>()
				.SingleInstance();
		}
	}
}