using Raven.Client;

namespace NotLimited.Framework.Identity.Raven
{
	public interface ISessionSource
	{
		IDocumentSession GetSession();
	}
}