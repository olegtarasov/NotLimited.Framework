using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using NotLimited.Framework.Common.Helpers;

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

		public static IQueryable<T> Filter<T>(this IQueryable<T> query, List<FilterDefinition> filterDefinitions)
		{
			if (filterDefinitions.IsNullOrEmpty())
				return query;

			var result = query;
			foreach (var filterDefinition in filterDefinitions)
			{
				string filterBy = filterDefinition.Property;
				var prop = PropertyMetadataCache<T>.GetPropertyMetadata(filterDefinition.Property);
				if (!string.IsNullOrEmpty(prop.FilterMember))
					filterBy += "." + prop.FilterMember;

				result = result.Where(filterBy + " = " + filterDefinition.Value);
			}

			return result;
		}
	}
}