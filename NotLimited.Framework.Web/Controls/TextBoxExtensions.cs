using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class TextBoxExtensions
	{
	    /// <summary>
	    /// Creates a text box.
	    /// </summary>
	    public static HelperResult TextBoxFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
	    {
	        return FormHelpers.TextBox(
	            helper.HtmlHelper.LabelFor(expression),
	            helper.HtmlHelper.TextBoxFor(expression, new { @class = "form-control" }),
	            helper.HtmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
	    }

	    /// <summary>
	    /// Creates a text area.
	    /// </summary>
	    public static HelperResult TextAreaFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, int lines = 5, int cols = 75)
	    {
	        return FormHelpers.TextBox(
	            helper.HtmlHelper.LabelFor(expression),
	            helper.HtmlHelper.TextAreaFor(expression, lines, cols, new { @class = "form-control" }),
	            helper.HtmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
	    }

	    /// <summary>
	    /// Creates a password field.
	    /// </summary>
	    public static HelperResult PasswordFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
	    {
	        return FormHelpers.TextBox(
	            helper.HtmlHelper.LabelFor(expression),
	            helper.HtmlHelper.PasswordFor(expression, new { @class = "form-control" }),
	            helper.HtmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
	    }
	}
}