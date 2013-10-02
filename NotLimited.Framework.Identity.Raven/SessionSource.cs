using System.Threading;
using Microsoft.AspNet.Identity;
using NotLimited.Framework.Raven;
using Raven.Client;

namespace NotLimited.Framework.Identity.Raven
{
	public class SessionSource : ISessionSource
	{
		private readonly RavenContext _context;

		private IDocumentSession _session = null;

		private static readonly object _locker = new object();

		public SessionSource(RavenContext context)
		{
			_context = context;
		}

		public IDocumentSession GetSession()
		{
			if (_session != null)
				return _session;

			lock (_locker)
			{
				if (_session == null)
				{
					var session = _context.OpenSession();
					Interlocked.Exchange(ref _session, session);
				}
			}

			return _session;
		}

		public bool SaveChanges()
		{
			if (_session == null)
				return true;

			lock (_locker)
			{
				if (_session == null)
					return true;

				_session.SaveChanges();
				_session.Dispose();
				Interlocked.Exchange(ref _session, null);
			}

			return true;
		}
	}
}