using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NotLimited.Framework.Raven;
using Raven.Client;

namespace NotLimited.Framework.Identity.Raven
{
	public class IdentityStore<T> : IIdentityStore, ISessionSource  where T : User
	{
		private readonly RavenContext _context;
		
		private IDocumentSession _session = null;
		
		private static readonly object _locker = new object();

		public IdentityStore(RavenContext context)
		{
			_context = context;
		}

		public void Dispose()
		{
		}

		public Task<IdentityResult> SaveChangesAsync(CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				if (_session == null)
					return new IdentityResult(true);

				lock (_locker)
				{
					if (_session == null)
						return new IdentityResult(true);

					_session.SaveChanges();
					_session.Dispose();
					Interlocked.Exchange(ref _session, null);
				}

				return new IdentityResult(true);
			}, cancellationToken);
		}

		internal UserStore<T> UserStore { get; private set; }

		public IUserSecretStore Secrets { get; private set; }
		public IUserLoginStore Logins { get; private set; }
		public IUserStore Users { get { return UserStore; } }
		public IUserManagementStore UserManagement { get; private set; }
		public IRoleStore Roles { get; private set; }
		public IUserClaimStore UserClaims { get; private set; }
		public ITokenStore Tokens { get; private set; }
		
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
	}
}