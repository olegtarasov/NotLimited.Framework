using NotLimited.Framework.Raven;

namespace NotLimited.Framework.Identity.Raven
{
	public abstract class StoreBase
	{
		protected readonly ISessionSource _sessionSource;

		protected StoreBase(ISessionSource sessionSource)
		{
			_sessionSource = sessionSource;
		}
	}
}