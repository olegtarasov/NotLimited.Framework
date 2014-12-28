//////////////////////////////////////////////////////////////////////////
// This file is a part of NotLimited.Framework.Common NuGet package.
// You are strongly discouraged from fiddling with it.
// If you do, all hell will break loose and living will envy the dead.
//////////////////////////////////////////////////////////////////////////
using System;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;

namespace $rootnamespace$.Helpers
{
	internal static class Lambda<T>
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