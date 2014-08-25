namespace NotLimited.Framework.Data.Entities
{
	public class UserBase : NamedEntityBase
	{
		public byte[] Password { get; set; }
		public byte[] Salt { get; set; }
		public string Email { get; set; }
	}
}