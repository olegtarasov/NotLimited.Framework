using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NotLimited.Framework.Raven;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserSecretStore : StoreBase, IUserSecretStore
	{
		public UserSecretStore(ISessionSource sessionSource) : base(sessionSource)
		{
		}

		public IUserSecret CreateNewInstance(string userName, string secret)
		{
			return new UserSecret
			                 {
				                 Secret = secret,
				                 UserName = userName
			                 };
		}

		public Task<IdentityResult> DeleteAsync(string userName, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var secret = session.Query<UserSecret>().FirstOrDefault(x => x.UserName == userName);
				if (secret == null)
					return new IdentityResult(false);

				session.Delete(secret);
				
				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> CreateAsync(IUserSecret userSecret, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				
				userSecret.Secret = Crypto.HashPassword(userSecret.Secret);
				session.Store(userSecret);
				
				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> UpdateAsync(string userName, string newSecret, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var secret = session.Query<UserSecret>().FirstOrDefault(x => x.UserName == userName);
				if (secret == null)
					return new IdentityResult(false);

				secret.Secret = Crypto.HashPassword(newSecret);
				session.Store(secret);
				
				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<bool> ValidateAsync(string userName, string loginSecret, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var secret = session.Query<UserSecret>().FirstOrDefault(x => x.UserName == userName);
				if (secret == null)
					return false;

				return Crypto.VerifyHashedPassword(secret.Secret, loginSecret);
			}, cancellationToken);
		}

		public Task<IUserSecret> FindAsync(string userName, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				
				return (IUserSecret)session.Query<UserSecret>().FirstOrDefault(x => x.UserName == userName);
			}, cancellationToken);
		}
	}
}