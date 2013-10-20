using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NotLimited.Framework.Raven;
using Raven.Client;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserStore<T> : IUserStore<T>, IUserSecurityStampStore<T>, IUserRoleStore<T>, IUserPasswordStore<T>, IUserLoginStore<T>, IUserClaimStore<T> where T : class, IUser
	{
		private readonly RavenContext _context;

		public UserStore(RavenContext context)
		{
			_context = context;
		}

		public void Dispose()
		{
		}

		public Task CreateAsync(T user)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					session.Store(user);
					session.SaveChanges();
				}
			});
		}

		public Task UpdateAsync(T user)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					session.Store(user);
					session.SaveChanges();
				}
			});
		}

		public Task DeleteAsync(T user)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var u = session.Load<T>(user.Id);
					if (u == null)
						return;

					session.Delete(u);
					session.SaveChanges();
				}
			});
		}

		public Task<T> FindByIdAsync(string userId)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					return session.Load<T>(userId);
				}
			});
		}

		public Task<T> FindByNameAsync(string userName)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					return session.Query<T>().FirstOrDefault(x => x.UserName == userName);
				}
			});
		}

		public Task SetSecurityStampAsync(T user, string stamp)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var s = session.Query<SecurityStamp>().FirstOrDefault(x => x.UserId == user.Id);
					if (s == null)
						s = new SecurityStamp {UserId = user.Id};

					s.Stamp = stamp;

					session.Store(s);
					session.SaveChanges();
				}
			});
		}

		public Task<string> GetSecurityStampAsync(T user)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var s = session.Query<SecurityStamp>().FirstOrDefault(x => x.UserId == user.Id);
					if (s == null)
						return null;

					return s.Stamp;
				}
			});
		}

		public Task AddToRoleAsync(T user, string role)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var userRole = session.Query<UserRole>().FirstOrDefault(x => x.UserId == user.Id && x.Role == role);
					if (userRole != null)
						return;

					session.Store(new UserRole {UserId = user.Id, Role = role});
					session.SaveChanges();
				}
			});
		}

		public Task RemoveFromRoleAsync(T user, string role)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var userRole = session.Query<UserRole>().FirstOrDefault(x => x.UserId == user.Id && x.Role == role);
					if (userRole == null)
						return;

					session.Delete(userRole);
					session.SaveChanges();
				}
			});
		}

		public Task<IList<string>> GetRolesAsync(T user)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					return (IList<string>)session.Query<UserRole>().Where(x => x.UserId == user.Id).Select(x => x.Role).ToList();
				}
			});
		}
		
		public Task<bool> IsInRoleAsync(T user, string role)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					return session.Query<UserRole>().Any(x => x.UserId == user.Id && x.Role == role);
				}
			});
		}

		public Task SetPasswordHashAsync(T user, string passwordHash)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var hash = session.Query<PasswordHash>().FirstOrDefault(x => x.UserId == user.Id);
					if (hash == null)
						hash = new PasswordHash {UserId = user.Id};

					hash.Hash = passwordHash;
					session.Store(hash);
					session.SaveChanges();
				}
			});
		}

		public Task<string> GetPasswordHashAsync(T user)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var hash = session.Query<PasswordHash>().FirstOrDefault(x => x.UserId == user.Id);
					return hash == null ? null : hash.Hash;
				}
			});
		}

		public Task<bool> HasPasswordAsync(T user)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					return session.Query<PasswordHash>().Any(x => x.UserId == user.Id);
				}
			});
		}

		public Task AddLoginAsync(T user, UserLoginInfo login)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var info = session.Query<UserLogin>().FirstOrDefault(x => x.UserId == user.Id);
					if (info == null)
						info = new UserLogin {UserId = user.Id};

					info.LoginInfo = login;

					session.Store(info);
					session.SaveChanges();
				}
			});
		}

		public Task RemoveLoginAsync(T user, UserLoginInfo login)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var info = session.Query<UserLogin>().FirstOrDefault(x => x.UserId == user.Id && x.LoginInfo.LoginProvider == login.LoginProvider && x.LoginInfo.ProviderKey == login.ProviderKey);
					if (info == null)
						return;

					session.Delete(info);
					session.SaveChanges();
				}
			});
		}

		public Task<IList<UserLoginInfo>> GetLoginsAsync(T user)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					return (IList<UserLoginInfo>)session.Query<UserLogin>().Where(x => x.UserId == user.Id).Select(x => x.LoginInfo).ToList();
				}
			});
		}

		public Task<T> FindAsync(UserLoginInfo login)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var info = session.Query<UserLogin>().FirstOrDefault(x => x.LoginInfo.LoginProvider == login.LoginProvider && x.LoginInfo.ProviderKey == login.ProviderKey);
					if (info == null)
						return null;

					return session.Load<T>(info.UserId);
				}
			});
		}

		public Task<IList<Claim>> GetClaimsAsync(T user)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					return (IList<Claim>)session.Query<UserClaim>().Where(x => x.UserId == user.Id).Select(x => x.Claim).ToList();
				}
			});
		}

		public Task AddClaimAsync(T user, Claim claim)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var c = session.Query<UserClaim>().FirstOrDefault(x => x.UserId == user.Id);
					if (c == null)
						c = new UserClaim { UserId = user.Id };

					c.Claim = claim;
					session.Store(c);
					session.SaveChanges();
				}
			});
		}

		public Task RemoveClaimAsync(T user, Claim claim)
		{
			return Task.Run(() =>
			{
				using (var session = _context.OpenSession())
				{
					var c = session.Query<UserClaim>().FirstOrDefault(x => x.UserId == user.Id && x.Claim.Type == claim.Type && x.Claim.Value == claim.Value);
					if (c == null)
						return;

					session.Delete(c);
					session.SaveChanges();
				}
			});
		}
	}
}