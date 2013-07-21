using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using NotLimited.Framework.Web.Controls.Builders.TextBox;

namespace NotLimited.Framework.Web.Controls
{
	public static class TextBoxExtensions
	{
		public static ITextBoxBuilder TextBoxFor<TModel, TValue>(this OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expr)
		{
			return new TextBoxBuilder<TModel, TValue>(helper, expr);
		}

		public static MvcHtmlString TextBox<TModel>(this OdinHelper<TModel> helper, string name, object value = null, string labelText = null, InputSize size = InputSize.Default, string helpText = null)
		{
			return helper.Input(name, helper.SimpleTextBox(name, value, size).ToString(), labelText, helpText);
		}

		public static MvcHtmlString SimpleTextBoxFor<TModel, TValue>(this OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expr, object value = null, InputSize size = InputSize.Default, string placeholder = null, bool password = false)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expr, helper.HtmlHelper.ViewData);
			return SimpleTextBox(helper, InputExtensions.GetControlName(helper.HtmlHelper, expr), value ?? metadata.Model, size, placeholder, password);
		}

		public static MvcHtmlString SimpleTextBox<TModel>(this OdinHelper<TModel> helper, string name, object value = null, InputSize size = InputSize.Default, string placeholder = null, bool password = false)
		{
			var textBox = new TagBuilder("input");

			textBox.MergeAttribute("type", password ? "password" : "text");
			textBox.MergeAttribute("name", name);
			textBox.MergeAttribute("id", name);
			if (value != null)
				textBox.MergeAttribute("value", value.ToString());
			if (size != InputSize.Default && size != InputSize.Block)
				textBox.AddCssClass("input-" + size.ToString());
			if (size == InputSize.Block)
				textBox.AddCssClass("input-block-level");
			if (!string.IsNullOrEmpty(placeholder))
				textBox.MergeAttribute("placeholder", placeholder);

			return new MvcHtmlString(textBox.ToString());
		}
	}
}