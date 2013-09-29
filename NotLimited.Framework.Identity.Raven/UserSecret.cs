using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserSecret : IUserSecret
	{
		public string UserName { get; set; }
		public string Secret { get; set; }
	}
}