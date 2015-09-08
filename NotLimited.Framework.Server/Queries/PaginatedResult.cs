using System.Collections.Generic;

namespace NotLimited.Framework.Server.Queries
{
	public class PaginatedResult<T>
	{
		public Pagination Pagination { get; set; }
		public IReadOnlyList<T> Items { get; set; }

		public static PaginatedResult<T> Empty(Pagination pagination)
		{
			return new PaginatedResult<T>
			       {
				       Pagination = pagination,
					   Items = new List<T>()
			       };
		}
	}
}