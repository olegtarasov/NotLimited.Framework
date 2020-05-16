using System;

namespace NotLimited.Framework.Server.Queries
{
	public class Pagination
	{
		public Pagination() : this(1, 20)
		{
		}

		public Pagination(int page) : this(page, 20)
		{
		}

		public Pagination(int page, int itemsPerPage)
		{
			Page = page;
			ItemsPerPage = itemsPerPage;
		}

		public int ItemsPerPage { get; set; }
		public int Page { get; set; }
		public int TotalCount { get; set; }

        public int PageCount { get { return (int)Math.Ceiling((double)TotalCount / ItemsPerPage); } }
	}
}