using System;
using System.Linq.Expressions;

namespace NotLimited.Framework.Data.Queries
{
	public class FilterDefinition
	{
		public FilterDefinition()
		{
		}

		public FilterDefinition(string property, string value)
		{
			Property = property;
			Value = value;
		}

		public string Property { get; set; }
		public string Value { get; set; }
	}
}