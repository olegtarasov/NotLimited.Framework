using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NotLimited.Framework.Raven;
using Raven.Client;

namespace NotLimited.Framework.Identity.Raven
{
	public class IdentityStore<T> : IIdentityStore where T : UserBase
	{
		private readonly ISessionSource _sessionSource = null;

		public IdentityStore(ISessionSource source)
		{
			_sessionSource = source;
		}

		public void Dispose()
		{
		}

		public Task<IdentityResult> SaveChangesAsync(CancellationToken cancellationToken)
		{
			return Task.Run(() => new IdentityResult(_sessionSource.SaveChanges()), cancellationToken);
		}

		public UserStore<T> UserStore { get; set; }

		public IUserSecretStore Secrets { get; set; }
		public IUserLoginStore Logins { get; set; }
		public IUserStore Users { get { return UserStore; } }
		public IUserManagementStore UserManagement { get; set; }
		public IRoleStore Roles { get; set; }
		public IUserClaimStore UserClaims { get; set; }
		public ITokenStore Tokens { get; set; }
		
		public IDocumentSession GetSession()
		{
			return _sessionSource.GetSession();
		}
	}
}