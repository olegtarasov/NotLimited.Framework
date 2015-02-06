using System.ComponentModel;
using NotLimited.Framework.Data.Queries;

namespace NotLimited.Framework.Data.Entities
{
	public abstract class NamedEntityBase : EntityBase
	{
		[DisplayName("Name")]
		[Description("��������")]
		[Sortable]
		public string Name { get; set; }
	}
}