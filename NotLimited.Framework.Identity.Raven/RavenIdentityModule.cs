using System.Linq;
using Autofac;

namespace NotLimited.Framework.Identity.Raven
{
	public class RavenIdentityModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterGeneric(typeof(UserStore<>))
			       .AsSelf();
		}
	}
}