using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserClaimStore : StoreBase, IUserClaimStore
	{
		public UserClaimStore(ISessionSource sessionSource) : base(sessionSource)
		{
		}

		public Task<IEnumerable<IUserClaim>> GetUserClaimsAsync(string userId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				return session.Query<UserClaim>().Where(x => x.UserId == userId).Cast<IUserClaim>().AsEnumerable();
			}, cancellationToken);
		}

		public Task<IdentityResult> AddAsync(IUserClaim userClaim, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				session.Store(userClaim);
				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> RemoveAsync(string userId, string claimType, string claimValue, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var claim = session.Query<UserClaim>().FirstOrDefault(x => x.UserId == userId && x.ClaimType == claimType && x.ClaimValue == claimValue);
				if (claim == null)
					return new IdentityResult(false);

				session.Delete(claim);

				return new IdentityResult(true);
			}, cancellationToken);
		}
	}
}