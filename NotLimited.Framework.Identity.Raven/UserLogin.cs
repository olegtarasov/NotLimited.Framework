using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserLogin : IUserLogin
	{
		public string UserId { get; set; }
		public string LoginProvider { get; set; }
		public string ProviderKey { get; set; }
	}
}