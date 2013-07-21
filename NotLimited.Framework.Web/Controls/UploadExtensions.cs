using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls
{
	public static class UploadExtensions
	{
		public static MvcHtmlString UploadFor<TModel, TValue>(this OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expr, string labelText = null, string helpText = null)
		{
			var textBox = new TagBuilder("input");

			InputExtensions.MergeNameAndId(textBox, helper.HtmlHelper, expr);
			textBox.MergeAttribute("type", "file");
			
			return helper.InputFor(expr, textBox.ToString(), labelText, helpText);
		}

		public static MvcHtmlString Upload<TModel>(this OdinHelper<TModel> helper, string name, string labelText = null, string helpText = null)
		{
			var textBox = new TagBuilder("input");

			textBox.MergeAttribute("type", "file");
			textBox.MergeAttribute("name", name);
			textBox.MergeAttribute("id", name);

			return helper.Input(name, textBox.ToString(), labelText, helpText);
		}
	}
}