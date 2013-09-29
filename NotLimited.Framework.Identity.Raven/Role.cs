using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class Role : IRole
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
}