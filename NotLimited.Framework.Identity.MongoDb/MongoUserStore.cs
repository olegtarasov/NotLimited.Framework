using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Identity.MongoDb
{
	public class MongoUserStore<TUser> : 
		MongoUserStore<TUser, string>,
		IUserStore<TUser>,
		IUserLoginStore<TUser>,
		IUserClaimStore<TUser>,
		IUserRoleStore<TUser>,
		IUserPasswordStore<TUser>,
		IUserSecurityStampStore<TUser>,
		IQueryableUserStore<TUser>,
		IUserConfirmationStore<TUser>,
		IUserEmailStore<TUser> where TUser : MongoUserBase<string>
	{
	}

	public class MongoUserStore<TUser, TKey> : 
		IUserLoginStore<TUser, TKey>,
		IUserClaimStore<TUser, TKey>,
		IUserRoleStore<TUser, TKey>,
		IUserPasswordStore<TUser, TKey>,
		IUserSecurityStampStore<TUser, TKey>,
		IQueryableUserStore<TUser, TKey>,
		IUserConfirmationStore<TUser, TKey>,
		IUserEmailStore<TUser, TKey> where TUser : MongoUserBase<TKey>
	{
		private readonly MongoDatabase _database;

		private MongoCollection<TUser> Collection { get { return _database.GetCollection<TUser>("Users"); } }

		public MongoUserStore() : this("DefaultConnection")
		{
		}

		public MongoUserStore(string connectionName)
		{
			var builder = new MongoUrlBuilder(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
			var settings = MongoClientSettings.FromUrl(builder.ToMongoUrl());
			_database = new MongoClient(settings).GetServer().GetDatabase(builder.DatabaseName);
		}

		public void Dispose()
		{
		}

		public Task CreateAsync(TUser user)
		{
			Collection.Save(user);
			return Task.FromResult(user);
		}

		public Task UpdateAsync(TUser user)
		{
			Collection.Save(user);
			return Task.FromResult(user);
		}

		public Task DeleteAsync(TUser user)
		{
			Collection.Remove(Query.EQ("_id", BsonValue.Create(user.Id)));
			return Task.FromResult(0);
		}

		public Task<TUser> FindByIdAsync(TKey userId)
		{
			return Task.FromResult(FindUserById(userId));
		}

		public Task<TUser> FindByNameAsync(string userName)
		{
			return Task.FromResult(FindUserByName(userName));
		}

		public Task AddLoginAsync(TUser user, UserLoginInfo login)
		{
			EnsureLoginList(user).Add(login);
			Collection.Save(user);
			return Task.FromResult(0);
		}

		public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
		{
			if (user.Logins != null)
			{ 
				user.Logins.RemoveAll(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
				Collection.Save(user);
			}

			return Task.FromResult(0);
		}

		public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
		{
			return Task.FromResult((IList<UserLoginInfo>)user.Logins.CreateEmptyIfNull());
		}

		public Task<TUser> FindAsync(UserLoginInfo login)
		{
			var user = Collection.AsQueryable().FirstOrDefault(x => x.Logins.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey));
			return Task.FromResult(user);
		}

		public Task<IList<Claim>> GetClaimsAsync(TUser user)
		{
			return Task.FromResult((IList<Claim>)user.Claims.EmptyIfNull().Select(x => x.ToClaim()).ToList());
		}

		public Task AddClaimAsync(TUser user, Claim claim)
		{
			EnsureClaimList(user).Add(new IdentityClaim(claim));
			Collection.Save(user);
			return Task.FromResult(0);
		}

		public Task RemoveClaimAsync(TUser user, Claim claim)
		{
			if (user.Claims != null)
			{
				user.Claims.RemoveAll(x => x.Type == claim.Type && x.Value == claim.Value);
				Collection.Save(user);
			}

			return Task.FromResult(0);
		}

		public Task AddToRoleAsync(TUser user, string roleName)
		{
			EnsureRoleList(user).Add(roleName);
			Collection.Save(user);
			return Task.FromResult(0);
		}

		public Task RemoveFromRoleAsync(TUser user, string roleName)
		{
			if (user.Roles != null)
			{
				user.Roles.Remove(roleName);
				Collection.Save(user);
			}

			return Task.FromResult(0);
		}

		public Task<IList<string>> GetRolesAsync(TUser user)
		{
			return Task.FromResult((IList<string>)user.Roles.CreateEmptyIfNull());
		}

		public Task<bool> IsInRoleAsync(TUser user, string roleName)
		{
			return Task.FromResult(user.Roles != null && user.Roles.Contains(roleName));
		}

		public Task SetPasswordHashAsync(TUser user, string passwordHash)
		{
			user.PasswordHash = passwordHash;
			Collection.Save(user);

			return Task.FromResult(0);
		}

		public Task<string> GetPasswordHashAsync(TUser user)
		{
			return Task.FromResult(user.PasswordHash);
		}

		public Task<bool> HasPasswordAsync(TUser user)
		{
			return Task.FromResult(string.IsNullOrEmpty(user.PasswordHash));
		}

		public Task SetSecurityStampAsync(TUser user, string stamp)
		{
			user.SecurityStamp = stamp;
			Collection.Save(user);

			return Task.FromResult(0);
		}

		public Task<string> GetSecurityStampAsync(TUser user)
		{
			return Task.FromResult(user.SecurityStamp);
		}

		public IQueryable<TUser> Users
		{
			get { return Collection.AsQueryable(); }
		}

		public Task<bool> IsConfirmedAsync(TUser user)
		{
			return Task.FromResult(user.IsConfirmed);
		}

		public Task SetConfirmedAsync(TUser user, bool confirmed)
		{
			user.IsConfirmed = confirmed;
			Collection.Save(user);

			return Task.FromResult(0);
		}

		public Task SetEmailAsync(TUser user, string email)
		{
			user.Email = email.ToLowerInvariant();
			Collection.Save(user);

			return Task.FromResult(0);
		}

		public Task<string> GetEmailAsync(TUser user)
		{
			return Task.FromResult(user.Email);
		}

		public Task<TUser> FindByEmailAsync(string email)
		{
			string lower = email.ToLowerInvariant();
			return Task.FromResult(Collection.AsQueryable().FirstOrDefault(x => x.Email == lower));
		}

		private TUser FindUserById(TKey id)
		{
			return Collection.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));
		}
		
		private TUser FindUserByName(string name)
		{
			return Collection.AsQueryable().FirstOrDefault(x => x.UserName == name);
		}

		private List<UserLoginInfo> EnsureLoginList(TUser user)
		{
			return user.Logins ?? (user.Logins = new List<UserLoginInfo>());
		}

		private List<IdentityClaim> EnsureClaimList(TUser user)
		{
			return user.Claims ?? (user.Claims = new List<IdentityClaim>());
		}

		private List<string> EnsureRoleList(TUser user)
		{
			return user.Roles ?? (user.Roles = new List<string>());
		}
	}
}