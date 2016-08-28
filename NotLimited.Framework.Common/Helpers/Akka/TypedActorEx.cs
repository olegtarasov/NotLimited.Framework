using Akka.Actor;

namespace NotLimited.Framework.Common.Helpers.Akka
{
	public class TypedActorEx : TypedActor
	{
		protected string SelfRemotePath => Self.Path.ToStringWithRemoteAddress(Context);
	}
}