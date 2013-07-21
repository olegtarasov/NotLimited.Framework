using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using NotLimited.Framework.Data;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Linq;

namespace NotLimited.Framework.Raven
{
	public static class RavenExtensions
	{
		#region PaginatedQuery

		public static PaginatedResult<T> PaginatedQuery<T>(this IDocumentSession session, Pagination pagination)
		{
			return session.Query<T>().PaginatedQuery(pagination);
		}

		public static PaginatedResult<T> PaginatedQuery<T>(this IRavenQueryable<T> query, Pagination originalPagination)
		{
			return PaginatedQuery<T, T>(query, originalPagination, null);
		}

		public static PaginatedResult<TResult> PaginatedQuery<TSource, TResult>(this IDocumentSession session, Pagination pagination, Func<TSource, TResult> transform)
		{
			return session.Query<TSource>().PaginatedQuery(pagination, transform);
		}

		public static PaginatedResult<TResult> PaginatedQuery<TSource, TResult>(this IRavenQueryable<TSource> query, Pagination originalPagination, Func<TSource, TResult> transform)
		{
			var pagination = originalPagination ?? new Pagination();

			RavenQueryStatistics stats;

			var items = query
				.Statistics(out stats)
				.Skip((pagination.Page - 1) * pagination.ItemsPerPage)
				.Take(pagination.ItemsPerPage)
				.AsEnumerable();

			var result = transform != null
				             ? items.Select(transform).ToList()
				             : items.Cast<TResult>().ToList();

			pagination.TotalCount = stats.TotalResults;

			return new PaginatedResult<TResult>
				{
					Pagination = pagination,
					Items = result
				};
		}

		#endregion

		public static void Clear<T>(this IDocumentSession session)
		{
			Clear<T>(session, null, documents => documents.Select(entity => new {}));
		}

		public static void Clear<T>(this IDocumentSession session, string indexName, Expression<Func<IEnumerable<T>, IEnumerable>> expression)
		{
			string name = string.IsNullOrEmpty(indexName) ? "remove_" + typeof(T).FullName : indexName;

			var index = session.Advanced.DocumentStore.DatabaseCommands.GetIndex(name);
			if (index == null)
			{
				session.Advanced.DocumentStore.DatabaseCommands.PutIndex(name, new IndexDefinitionBuilder<T>
					{
						Map = expression
					});

				while (session.Advanced.DocumentStore.DatabaseCommands.GetStatistics().StaleIndexes.Length > 0)
				{
					Thread.Sleep(10);
				}
			}

			session.Advanced.DocumentStore.DatabaseCommands.DeleteByIndex(name, new IndexQuery());
		}
	}
}