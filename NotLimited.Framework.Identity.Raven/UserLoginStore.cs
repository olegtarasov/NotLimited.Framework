using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NotLimited.Framework.Raven;
using Raven.Client.Linq;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserLoginStore : StoreBase, IUserLoginStore
	{
		public UserLoginStore(ISessionSource sessionSource) : base(sessionSource)
		{
		}

		public IUserLogin CreateNewInstance(string userId, string loginProvider, string providerKey)
		{
			return new UserLogin
			                {
				                LoginProvider = loginProvider,
				                ProviderKey = providerKey,
				                UserId = userId
			                };
		}

		public Task<string> GetUserIdAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var rs = _sessionSource.GetSession();
				var login = rs.Query<UserLogin>().FirstOrDefault(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);

				return login != null ? login.UserId : null;
			}, cancellationToken);
		}

		public Task<string> GetProviderKeyAsync(string userId, string loginProvider, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var rs = _sessionSource.GetSession();
				var login = rs.Query<UserLogin>().FirstOrDefault(x => x.LoginProvider == loginProvider && x.UserId == userId);

				return login != null ? login.ProviderKey : null;
			}, cancellationToken);
		}

		public Task<IdentityResult> AddAsync(IUserLogin login, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var rs = _sessionSource.GetSession();
				rs.Store(login);
				
				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> RemoveAsync(string userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var login = session.Query<UserLogin>().FirstOrDefault(x => x.UserId == userId && x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
				if (login == null)
					return new IdentityResult(false);

				session.Delete(login);
				
				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IEnumerable<IUserLogin>> GetLoginsAsync(string userId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				
				return (IEnumerable<IUserLogin>)session.Query<UserLogin>().Where(x => x.UserId == userId).ToList();
			}, cancellationToken);
		}
	}
}