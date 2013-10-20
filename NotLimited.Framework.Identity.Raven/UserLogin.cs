using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserLogin
	{
		public string UserId { get; set; }
		public UserLoginInfo LoginInfo { get; set; }
	}
}