using Akka.Actor;

namespace NotLimited.Framework.Common.Helpers.Akka
{
	public static class AkkaExtensions
	{
		public static string ToStringWithRemoteAddress(this ActorPath path, IActorContext context)
		{
			return path.ToStringWithAddress(context.GetDefaultAddres());
		}

		public static Address GetDefaultAddres(this IActorContext ctx)
		{
			return ((ExtendedActorSystem)ctx.System).Provider.DefaultAddress;
		}
	}
}