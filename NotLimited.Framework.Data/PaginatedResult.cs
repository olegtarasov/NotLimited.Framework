using System.Collections.Generic;

namespace NotLimited.Framework.Data
{
	public class PaginatedResult<T>
	{
		public Pagination Pagination { get; set; }
		public List<T> Items { get; set; }
	}
}