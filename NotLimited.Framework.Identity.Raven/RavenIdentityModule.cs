using System.Linq;
using Autofac;

namespace NotLimited.Framework.Identity.Raven
{
	public class RavenIdentityModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var types = ThisAssembly.GetTypes();
			var stores = types.Where(x => x.IsInstanceOfType(typeof(StoreBase)) && !x.IsGenericType).ToArray();

			builder
				.RegisterTypes(stores)
				.AsImplementedInterfaces();

			builder.RegisterGeneric(typeof(UserStore<>))
			       .AsSelf();

			builder
				.RegisterGeneric(typeof(IdentityStore<>))
				.AsSelf()
				.As<ISessionSource>()
				.PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
				.SingleInstance();
		}
	}
}