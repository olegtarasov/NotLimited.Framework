using System;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;

namespace NotLimited.Framework.Common.Helpers
{
	public static class Lambda<T>
	{
		public static Expression<Func<T, TKey>> Expr<TKey>(Expression<Func<T, TKey>> expression)
		{
			return expression;
		}

		public static string MemberName<TKey>(Expression<Func<T, TKey>> expression)
		{
			return expression.GetMemberName();
		}
	}
}