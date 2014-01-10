using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.MongoDb
{
	public class UserBase<TKey> : IUser<TKey>
	{
		public TKey Id { get; private set; }
		public string UserName { get; set; }
	}
}