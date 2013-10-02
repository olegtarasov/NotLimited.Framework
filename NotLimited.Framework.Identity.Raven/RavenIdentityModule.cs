using System.Linq;
using Autofac;

namespace NotLimited.Framework.Identity.Raven
{
	public class RavenIdentityModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var types = ThisAssembly.GetTypes();
			var stores = types.Where(x => x.IsSubclassOf(typeof(StoreBase)) && !x.IsGenericType).ToArray();

			builder
				.RegisterType<SessionSource>()
				.As<ISessionSource>()
				.SingleInstance();

			builder
				.RegisterTypes(stores)
				.AsImplementedInterfaces();

			builder.RegisterGeneric(typeof(UserStore<>))
			       .AsSelf();

			builder
				.RegisterGeneric(typeof(IdentityStore<>))
				.PropertiesAutowired()
				.SingleInstance();
		}
	}
}