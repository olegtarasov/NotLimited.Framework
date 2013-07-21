using System.ComponentModel;

namespace NotLimited.Framework.Data.Entities
{
	public abstract class NamedEntityBase : EntityBase
	{
		[DisplayName("Name")]
		public string Name { get; set; }
	}
}