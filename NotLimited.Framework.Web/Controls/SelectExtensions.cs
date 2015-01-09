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
using NotLimited.Framework.Web.Controls.Builders;
using NotLimited.Framework.Web.Helpers;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class SelectExtensions
	{
        /// <summary>
        /// Transfoms a collection into a list of SelectListItems.
        /// </summary>
        /// <param name="source">Source collection</param>
        /// <param name="nameExpr">Function that extracts a title from an element.</param>
        /// <param name="valueExpr">Function that extracts a value from an element.</param>
        /// <param name="selected">Selected value.</param>
        /// <param name="includeUnselected">Include and "Unselected" item.</param>
        /// <param name="unselectedText">"Unselected" item text.</param>
        public static List<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> source, Expression<Func<T, string>> nameExpr, Expression<Func<T, string>> valueExpr, object selected = null, bool includeUnselected = false, string unselectedText = "[ Не выбрано ]")
		{
			var result = new List<SelectListItem>();
			var nameFunc = nameExpr.Compile();
			var valFunc = valueExpr.Compile();

            if (includeUnselected)
            {
                result.Add(new SelectListItem {Text = unselectedText, Value = String.Empty, Selected = selected == null});
            }

            result.AddRange(source.Select(item => new SelectListItem
													{
														Text = nameFunc(item), 
														Value = valFunc(item), 
														Selected = (selected != null && selected.ToString() == valFunc(item))
													}));

			return result;
		}

        /// <summary>
        /// Appends an empty item to a list of SelectListItems.
        /// </summary>
        public static List<SelectListItem> WithEmptyItem(this IEnumerable<SelectListItem> source)
	    {
	        var result = new List<SelectListItem> {new SelectListItem()};
            result.AddRange(source);

	        return result;
	    }

	    /// <summary>
	    /// Creates a select control.
	    /// </summary>
	    public static SelectBuilder<TModel, TValue> DropDownFor<TModel, TValue>(this FormHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items)
	    {
	        return new SelectBuilder<TModel, TValue>(helper.HtmlHelper, expression)
                .Items(items)
                .Type(SelectControlType.DropDown);
	    }

	    /// <summary>
	    /// Creates a list box control.
	    /// </summary>
        public static SelectBuilder<TModel, TValue> ListBoxFor<TModel, TValue>(this FormHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items)
	    {
	        return new SelectBuilder<TModel, TValue>(helper.HtmlHelper, expression)
                .Items(items)
                .Type(SelectControlType.ListBox);
	    }

	    /// <summary>
	    /// Creates a dropdown control for an enum.
	    /// </summary>
        public static SelectBuilder<TModel, TProperty> EnumDropDownFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
	    {
	        return new SelectBuilder<TModel, TProperty>(helper.HtmlHelper, expression)
                .Type(SelectControlType.EnumDropDown);
	    }
	}
}