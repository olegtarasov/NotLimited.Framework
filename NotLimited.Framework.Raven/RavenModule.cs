using Autofac;

namespace NotLimited.Framework.Raven
{
	public class RavenModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.RegisterType<RavenContext>()
				.AsSelf()
				.SingleInstance()
				.WithParameter("connectionStringName", "default");
		}
	}
}