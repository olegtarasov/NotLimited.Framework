using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class UploadExtensions
	{
	    /// <summary>
	    /// Creates an upload control.
	    /// </summary>
	    public static HelperResult UploadFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string accept = "image/*")
	    {
	        return FormHelpers.Upload(
	            helper.HtmlHelper.LabelFor(expression),
	            helper.HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression)),
	            helper.HtmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" }),
				accept);
	    }

	    /// <summary>
	    /// Creates an upload control with custom name and label.
	    /// </summary>
	    public static HelperResult Upload<TModel>(this FormHelper<TModel> helper, string name, string label, string accept = "image/*")
	    {
	        return FormHelpers.Upload(
	            helper.HtmlHelper.Label(name, label),
	            name,
	            new MvcHtmlString(""),
                accept);
	    }
	}
}