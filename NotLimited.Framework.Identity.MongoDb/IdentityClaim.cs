using System.Security.Claims;

namespace NotLimited.Framework.Identity.MongoDb
{
	public class IdentityClaim
	{
		public IdentityClaim()
		{
		}

		public IdentityClaim(Claim claim)
		{
			Type = claim.Type;
			Value = claim.Value;
		}

		public string Type { get; set; }
		public string Value { get; set; }

		public Claim ToClaim()
		{
			return new Claim(Type, Value);
		}
	}
}