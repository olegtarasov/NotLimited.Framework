using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls
{
	public static class InputExtensions
	{
		public static MvcHtmlString InputFor<TModel, TValue>(this OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expr, string controlHtml, string labelText = null, string helpText = null)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expr, helper.HtmlHelper.ViewData);
			return Input(helper, metadata.PropertyName, controlHtml, labelText ?? metadata.DisplayName, helpText);
		}

		public static MvcHtmlString Input(this OdinHelper helper, string name, string controlHtml, string labelText = null, string helpText = null)
		{
			var clear = new TagBuilder("div");
			var label = new TagBuilder("label");
			var input = new TagBuilder("div");

			clear.AddCssClass("control-group");
			label.AddCssClass("control-label");
			label.MergeAttribute("for", name);
			label.SetInnerText(labelText ?? name);
			input.AddCssClass("controls");
			input.InnerHtml = controlHtml;

			if (!string.IsNullOrEmpty(helpText))
			{
				var help = new TagBuilder("span");
				help.AddCssClass("help-block");
				help.InnerHtml = helpText;
				input.InnerHtml += help.ToString();
			}

			clear.InnerHtml = label.ToString() + input.ToString();

			return new MvcHtmlString(clear.ToString());
		}

		public static void MergeNameAndId<TModel, TValue>(this TagBuilder builder, HtmlHelper helper, Expression<Func<TModel, TValue>> expr)
		{
			string name = GetControlName(helper, expr);
			builder.MergeAttribute("name", name);
			builder.MergeAttribute("id", name);
		}

		public static string GetControlName<TModel, TValue>(this HtmlHelper helper, Expression<Func<TModel, TValue>> expr)
		{
			return helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expr));
		}
	}
}