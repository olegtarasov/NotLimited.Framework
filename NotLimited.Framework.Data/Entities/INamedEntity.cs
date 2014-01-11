namespace NotLimited.Framework.Data.Entities
{
	public interface INamedEntity : IEntity
	{
		string Name { get; set; }
	}
}