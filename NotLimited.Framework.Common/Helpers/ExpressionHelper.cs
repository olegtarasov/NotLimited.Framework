using System;
using System.Linq.Expressions;

namespace NotLimited.Framework.Common.Helpers
{
	public static class ExpressionHelper
	{
		public static string GetMemberName<TModel, TKey>(this Expression<Func<TModel, TKey>> expression)
		{
			var memberExpr = expression.Body as MemberExpression;
			if (memberExpr == null)
				throw new InvalidOperationException("Invalid expression!");

			return memberExpr.Member.Name;
		}
	}
}