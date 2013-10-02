using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NotLimited.Framework.Raven;

namespace NotLimited.Framework.Identity.Raven
{
	public class RoleStore : StoreBase, IRoleStore
	{
		public RoleStore(ISessionSource sessionSource) : base(sessionSource)
		{
		}

		public Task<IdentityResult> CreateRoleAsync(IRole role, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();

				session.Store(role);

				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> DeleteRoleAsync(string roleId, bool failIfNonEmpty, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var role = session.Load<Role>(roleId);
				if (role == null)
					return new IdentityResult(!failIfNonEmpty);

				session.Delete(role);

				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IRole> FindRoleAsync(string roleId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				return (IRole)session.Load<Role>(roleId);
			}, cancellationToken);
		}

		public Task<IRole> FindRoleByNameAsync(string roleName, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				return (IRole)session.Query<Role>().FirstOrDefault(x => x.Name == roleName);
			}, cancellationToken);
		}

		public Task<bool> RoleExistsAsync(string roleId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				return session.Load<Role>(roleId) != null;
			}, cancellationToken);
		}

		public Task<IdentityResult> AddUserToRoleAsync(string userId, string roleId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var userRole = session.Query<UserRole>().FirstOrDefault(x => x.UserId == userId && x.RoleId == roleId);

				if (userRole != null)
					return new IdentityResult(true);

				session.Store(new UserRole {UserId = userId, RoleId = roleId});

				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IdentityResult> RemoveUserFromRoleAsync(string userId, string roleId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var userRole = session.Query<UserRole>().FirstOrDefault(x => x.UserId == userId && x.RoleId == roleId);

				if (userRole == null)
					return new IdentityResult(false);

				session.Delete(userRole);

				return new IdentityResult(true);
			}, cancellationToken);
		}

		public Task<IEnumerable<IRole>> GetRolesForUserAsync(string userId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				var userRoles = session.Query<UserRole>().Where(x => x.UserId == userId).Select(x => x.RoleId);

				return (IEnumerable<IRole>)session.Load<Role>(userRoles).AsEnumerable();
			}, cancellationToken);
		}

		public Task<IEnumerable<string>> GetUsersInRoleAsync(string roleId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				return session.Query<UserRole>().Where(x => x.RoleId == roleId).Select(x => x.UserId).AsEnumerable();
				
			}, cancellationToken);
		}

		public Task<bool> IsUserInRoleAsync(string userId, string roleId, CancellationToken cancellationToken)
		{
			return Task.Run(() =>
			{
				var session = _sessionSource.GetSession();
				return session.Query<UserRole>().Any(x => x.UserId == userId && x.RoleId == roleId);
			}, cancellationToken);
		}
	}
}