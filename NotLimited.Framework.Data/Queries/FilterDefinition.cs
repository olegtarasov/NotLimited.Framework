using System;
using System.Linq.Expressions;

namespace NotLimited.Framework.Data.Queries
{
	public class FilterDefinition
	{
		public LambdaExpression Expression { get; set; }
		public string Value { get; set; }
	}
}