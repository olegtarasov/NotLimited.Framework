using System;
using System.Linq;
using System.Linq.Dynamic;

namespace NotLimited.Framework.Data.Queries
{
	public static class QuryableExtensions
	{
		public static IQueryable<T> Sort<T>(this IQueryable<T> query, SortDefinition sortDefinition)
		{
			if (sortDefinition == null || string.IsNullOrEmpty(sortDefinition.SortBy))
				return query;

			string sortBy = sortDefinition.SortBy;
			var prop = PropertyMetadataCache<T>.GetPropertyMetadata(sortDefinition.SortBy);
			if (!string.IsNullOrEmpty(prop.SortMember))
				sortBy += "." + prop.SortMember;

			return query.OrderBy(sortBy + " " + (sortDefinition.Descending ? "desc" : "asc"));
		}
	}
}