using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserClaim : IUserClaim
	{
		public string UserId { get; set; }
		public string ClaimType { get; set; }
		public string ClaimValue { get; set; }
	}
}