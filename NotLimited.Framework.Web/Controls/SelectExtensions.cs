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
using NotLimited.Framework.Data.Queries;
using NotLimited.Framework.Web.Controls.Builders;
using NotLimited.Framework.Web.Helpers;
using NotLimited.Framework.Web.Mvc;
using NotLimited.Framework.Web.Views.Shared.Helpers;
using EnumHelper = System.Web.Mvc.Html.EnumHelper;

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
	    /// <param name="enabledValues">Values that are enabled in a box.</param>
	    public static List<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> source, Expression<Func<T, string>> nameExpr, Expression<Func<T, string>> valueExpr, object selected = null, bool includeUnselected = false, string unselectedText = "[ Не выбрано ]", HashSet<string> enabledValues = null)
		{
			var result = new List<SelectListItem>();
			var nameFunc = nameExpr.Compile();
			var valFunc = valueExpr.Compile();

            if (includeUnselected)
            {
                result.Add(new SelectListItem {Text = unselectedText, Value = String.Empty, Selected = selected == null});
            }

	        result.AddRange(source.Select(item =>
	        {
	            var value = valFunc(item);
	            return new SelectListItem
	                   {
	                       Text = nameFunc(item),
	                       Value = value,
	                       Selected = (selected != null && selected.ToString() == valFunc(item)),
	                       Disabled = enabledValues != null && !enabledValues.Contains(value)
	                   };
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
        /// Renders an “Items per page” dropdown.
        /// </summary>
        public static MvcHtmlString ItemsPerPageDropdown(this FormHelper helper, int? page)
	    {
	        var list = new List<SelectListItem>
	                   {
	                       new SelectListItem {Text = "20", Value = "20", Selected = page.GetValueOrDefault() == 20},
	                       new SelectListItem {Text = "50", Value = "50", Selected = page.GetValueOrDefault() == 50},
	                       new SelectListItem {Text = "100", Value = "100", Selected = page.GetValueOrDefault() == 100}
	                   };

            return helper.HtmlHelper.DropDownList(Lambda<Pagination>.MemberName(x => x.ItemsPerPage), list, new { @class = "form-control", onchange = "SetItemsPerPage();", style = "display: inline-block; width: 75px; margin-left: 20px;" });
	    }

        /// <summary>
        /// Unselects all items in a collection.
        /// </summary>
        public static IEnumerable<SelectListItem> UnselectAll(this IEnumerable<SelectListItem> source)
	    {
	        foreach (var item in source)
	        {
	            item.Selected = false;
	        }

	        return source;
	    }


		/// <summary>
		/// Marks single item as selected.
		/// </summary>
		public static List<SelectListItem> SelectSingle(this IEnumerable<SelectListItem> source, string value)
		{
			var result = source.Select(x => x.Clone()).ToList();
			bool selected = false;
			foreach (var item in result)
			{
				if (item.Value == value && !selected)
				{
					selected = true;
					item.Selected = true;
				}
				else
				{
					item.Selected = false;
				}
			}

			return result;
		}

		public static SelectListItem Clone(this SelectListItem item)
		{
			return new SelectListItem {Text = item.Text, Value = item.Value, Disabled = item.Disabled, Group = item.Group, Selected = item.Selected};
		}

		public static SelectBuilder<TModel, TValue> DropDownFor<TModel, TValue>(this FormHelper helper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items) where TModel : new()
	    {
	        var typedHelper = helper.HtmlHelper as HtmlHelper<TModel>;
	        if (typedHelper == null)
	        {
                var container = new DummyViewDataContainer(new ViewDataDictionary<TModel>(new TModel()));
                typedHelper = new HtmlHelper<TModel>(helper.HtmlHelper.ViewContext, container);
	        }

            return new SelectBuilder<TModel, TValue>(typedHelper, expression)
                .Items(items)
                .Type(SelectControlType.DropDown);
	    }

	    /// <summary>
	    /// Creates a select control.
	    /// </summary>
	    public static SelectBuilder<TModel, TValue> DropDownFor<TModel, TValue>(this FormHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items) where TModel : new()
	    {
	        return DropDownFor((FormHelper)helper, expression, items);
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

	    public static IList<SelectListItem> GetEnumList<T>()
	    {
	        return EnumHelper.GetSelectList(typeof(T));
	    }
	}
}