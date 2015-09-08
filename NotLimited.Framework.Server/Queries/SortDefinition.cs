namespace NotLimited.Framework.Server.Queries
{
	public class SortDefinition
	{
	    public SortDefinition()
	    {
	    }

	    public SortDefinition(string sortBy, bool descending)
	    {
	        SortBy = sortBy;
	        Descending = descending;
	    }

	    public string SortBy { get; set; }
		public bool Descending { get; set; }
	}
}