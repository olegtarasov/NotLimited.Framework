using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls
{
	public static class TextAreaExtensions
	{
		public static MvcHtmlString TextAreaFor<TModel, TValue>(this OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expr, int rows = 3, string labelText = null, InputSize size = InputSize.Default, string helpText = null)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expr, helper.HtmlHelper.ViewData);
			var textBox = new TagBuilder("textarea");

			InputExtensions.MergeNameAndId(textBox, helper.HtmlHelper, expr);
			textBox.MergeAttribute("rows", rows.ToString());
			if (metadata.Model != null)
				textBox.InnerHtml = metadata.Model.ToString();
			if (size != InputSize.Default)
				textBox.AddCssClass("input-" + size.ToString());

			return helper.InputFor(expr, textBox.ToString(), labelText, helpText);
		}
	}
}