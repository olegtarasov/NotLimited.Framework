using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserManagementStore : StoreBase, IUserManagementStore
	{
		public UserManagementStore(ISessionSource sessionSource) : base(sessionSource)
		{
		}

		public IUserManagement CreateNewInstance(string userId)
		{
			return new UserManagement
			       {
				       DisableSignIn = false,
				       LastSignInTimeUtc = DateTime.MinValue,
				       UserId = userId
			       };
		}

		public Task<IUserManagement> FindAsync(string userId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();

				return (IUserManagement)session.Query<UserManagement>().FirstOrDefault(x => x.UserId == userId);
			}, cancellationToken);
		}

		public Task<IdentityResult> CreateAsync(IUserManagement info, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();

				session.Store(info);

				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> UpdateAsync(IUserManagement info, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var management = session.Query<UserManagement>().FirstOrDefault(x => x.UserId == info.UserId);
				if (management == null)
					return new IdentityResult(false);

				management.DisableSignIn = info.DisableSignIn;
				management.LastSignInTimeUtc = info.LastSignInTimeUtc;
				session.Store(management);

				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> DeleteAsync(string userId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var management = session.Query<UserManagement>().FirstOrDefault(x => x.UserId == userId);
				if (management == null)
					return new IdentityResult(false);

				session.Delete(management);

				return new IdentityResult(true);
			}, cancellationToken);
		}
	}
}