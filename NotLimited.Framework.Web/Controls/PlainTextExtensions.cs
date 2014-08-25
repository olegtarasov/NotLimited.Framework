using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Autofac.Core;
using NotLimited.Framework.Web.Controls.Builders.Image;

namespace NotLimited.Framework.Web.Controls
{
	public static class PlainTextExtensions
	{
		public static MvcHtmlString PlainText<TModel>(this OdinHelper<TModel> helper, string name, string value, string labelText = null, InputSize size = InputSize.Default, string helpText = null)
		{
			var div = new TagBuilder("div");

			div.AddCssClass("form-field-plaintext");
			div.InnerHtml = value;

			return helper.Input(name, div.ToString(), labelText, helpText);
		}

		public static IImageBuilder ImageFor<TModel, TValue>(this OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expr)
		{
			return new ImageBuilder<TModel, TValue>(helper, expr);
		}
	}
}