using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserStore<T> : StoreBase, IUserStore where T : User
	{
		public UserStore(ISessionSource sessionSource) : base(sessionSource)
		{
		}

		public Task<IUser> FindAsync(string userId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();

				return (IUser)session.Query<T>().FirstOrDefault(x => x.Id == userId);
			}, cancellationToken);
		}

		public Task<IUser> FindByNameAsync(string userName, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();

				return (IUser)session.Query<T>().FirstOrDefault(x => x.UserName == userName);
			}, cancellationToken);
		}

		public Task<IdentityResult> CreateAsync(IUser user, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();

				session.Store(user);

				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> DeleteAsync(string userId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var user = session.Query<T>().FirstOrDefault(x => x.Id == userId);
				if (user == null)
					return new IdentityResult(false);

				session.Delete(user);

				return new IdentityResult(true);
			}, cancellationToken);
		}
	}
}