namespace NotLimited.Framework.Data.Entities
{
	public abstract class EntityBase : IEntity
	{
		public virtual long Id { get; set; }
	}
}