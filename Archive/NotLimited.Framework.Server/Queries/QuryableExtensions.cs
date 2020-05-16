using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Server.Queries
{
	public static class QuryableExtensions
	{
		public static IQueryable<T> Sort<T>(this IQueryable<T> query, SortDefinition sortDefinition)
		{
			if (sortDefinition == null || String.IsNullOrEmpty(sortDefinition.SortBy))
				return query;

			string sortBy = sortDefinition.SortBy;
			var prop = PropertyMetadataCache<T>.GetPropertyMetadata(sortDefinition.SortBy);
			if (!String.IsNullOrEmpty(prop.SortMember))
				sortBy += "." + prop.SortMember;

			return query.OrderBy(sortBy + " " + (sortDefinition.Descending ? "desc" : "asc"));
		}

		public static IQueryable<T> Filter<T>(this IQueryable<T> query, List<FilterDefinition> filterDefinitions)
		{
			if (filterDefinitions.IsNullOrEmpty())
				return query;

			var result = query;
			foreach (var filterDefinition in filterDefinitions)
			{
				string filterBy = filterDefinition.Property;
				var prop = PropertyMetadataCache<T>.GetPropertyMetadata(filterDefinition.Property);
				if (!String.IsNullOrEmpty(prop.FilterMember))
					filterBy += "." + prop.FilterMember;

				result = result.Where(filterBy + " = " + filterDefinition.Value);
			}

			return result;
		}

	    public static PaginatedResult<T> Paginate<T>(this IQueryable<T> query, Pagination pagination, SortDefinition sort = null)
	    {
	        return Paginate(query, pagination, x => x, sort);
	    }

        public static PaginatedResult<TDest> Paginate<TSource, TDest>(this IQueryable<TSource> query, Pagination pagination, Func<TSource, TDest> map, SortDefinition sort = null)
	    {
	        if (query == null)
	            return null;

            if (map == null) throw new ArgumentNullException(nameof(map));
            var result = new PaginatedResult<TDest>();
			var set = query;

	        set = !string.IsNullOrEmpty(sort?.SortBy) ? set.Sort(sort) : set.OrderBy("Id");

	        if (pagination != null)
	        {
				result.Pagination = pagination;
				result.Pagination.TotalCount = set.Count();

				set = set.Skip(pagination.ItemsPerPage * (pagination.Page - 1))
						.Take(pagination.ItemsPerPage);
			}

			result.Items = set.AsEnumerable().Select(map).ToList();
	        
	        return result;
	    }
	}
}