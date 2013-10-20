using System.Security.Claims;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserClaim
	{
		public string UserId { get; set; }
		public Claim Claim { get; set; }
	}
}