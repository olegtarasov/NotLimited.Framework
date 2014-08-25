using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotLimited.Framework.Identity.MongoDb
{
	public class MongoUserBase<TKey> : IUser<TKey>
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public TKey Id { get; set; }
		public string UserName { get; set; }

		public List<MongoLoginInfo> Logins { get; set; }
		public List<IdentityClaim> Claims { get; set; }
		public List<string> Roles { get; set; }
		public string PasswordHash { get; set; }
		public string SecurityStamp { get; set; }
		public bool IsConfirmed { get; set; }
		public string Email { get; set; }

		public void SetLogin(MongoLoginInfo login)
		{
			if (Logins == null)
				Logins = new List<MongoLoginInfo>();

			var existingLogin = Logins.Find(x => x.ProviderKey == login.ProviderKey);
			if (existingLogin != null)
				Logins.Remove(existingLogin);

			Logins.Add(login);
		}
	}
}