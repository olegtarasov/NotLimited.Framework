using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using NotLimited.Framework.Web.Controls.Builders.TextArea;

namespace NotLimited.Framework.Web.Controls
{
	public static class TextAreaExtensions
	{
		public static ITextAreaBuilder TextAreaFor<TModel, TValue>(this OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expr)
		{
			return new TextAreaBuilder<TModel, TValue>(helper, expr);
		}
	}
}