using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using NotLimited.Framework.Common.Helpers;
using NotLimited.Framework.Web.Helpers;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class SelectExtensions
	{
		public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> source, Expression<Func<T, string>> nameExpr, Expression<Func<T, string>> valueExpr, object selected = null, bool excludeUnselected = false, string unselectedText = "[ Не выбрано ]")
		{
			var result = new List<SelectListItem>();
			var nameFunc = nameExpr.Compile();
			var valFunc = valueExpr.Compile();

			if (!excludeUnselected)
				result.Add(new SelectListItem { Text = unselectedText, Value = String.Empty, Selected = selected == null });

			result.AddRange(source.Select(item => new SelectListItem
													{
														Text = nameFunc(item), 
														Value = valFunc(item), 
														Selected = (selected != null && selected.ToString() == valFunc(item))
													}));

			return result;
		}

	    /// <summary>
	    /// Creates a select control.
	    /// </summary>
	    public static HelperResult SelectFor<TModel, TValue>(this FormHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes)
	    {
	        return FormHelpers.TextBox(
	            helper.HtmlHelper.LabelFor(expression),
	            helper.HtmlHelper.DropDownListFor(expression, items, new { @class = "form-control" }.ConcatHtmlAttributes(htmlAttributes)),
	            helper.HtmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
	    }

	    /// <summary>
	    /// Creates a list box control.
	    /// </summary>
	    public static HelperResult ListBoxFor<TModel, TValue>(this FormHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes)
	    {
	        return FormHelpers.TextBox(
	            helper.HtmlHelper.LabelFor(expression),
	            helper.HtmlHelper.ListBoxFor(expression, items, new { @class = "form-control" }.ConcatHtmlAttributes(htmlAttributes)),
	            helper.HtmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
	    }

	    /// <summary>
	    /// Creates a dropdown control for an enum.
	    /// </summary>
	    public static HelperResult EnumDropDownFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
	    {
	        return FormHelpers.TextBox(
	            helper.HtmlHelper.LabelFor(expression),
	            helper.HtmlHelper.EnumDropDownListFor(expression, new { @class = "form-control" }),
	            helper.HtmlHelper.ValidationMessageFor(expression, "", new { @class = "text-danger" }));
	    }
	}
}