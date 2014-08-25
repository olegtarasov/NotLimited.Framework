using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NotLimited.Framework.Common.Helpers;
using NotLimited.Framework.Web.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class SelectExtensions
	{
		public static MvcHtmlString EnumComboFor<TModel, TEnum>(this OdinHelper<TModel> helper, Expression<Func<TModel, TEnum>> expr, TEnum value, string labelText = null, InputSize size = InputSize.Default, string helpText = null) where TModel : class
		{
			return helper.SelectFor(expr, GetEnumItems(value), labelText, size, helpText);
		}

		public static MvcHtmlString EnumCombo<TModel, TEnum>(this OdinHelper<TModel> helper, string name, TEnum value, string onChange = null, string labelText = null, InputSize size = InputSize.Default, string helpText = null) where TModel : class
		{
			return helper.Select(name, GetEnumItems(value), labelText, onChange, size, helpText);
		}

		private static List<SelectListItem> GetEnumItems<TEnum>(TEnum value)
		{
			var selectedValue = (int)(object)value;
			var dic = EnumHelper.GetEnumDictionary<TEnum>();
			var list = new List<SelectListItem>(dic.Count);

			list.AddRange(from pair in dic
						  let val = (int)(object)pair.Key
						  select new SelectListItem
						  {
							  Selected = (val == selectedValue),
							  Text = pair.Value,
							  Value = pair.Key.ToString()
						  });

			return list;
		}

		public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> source, Expression<Func<T, string>> nameExpr, Expression<Func<T, string>> valueExpr, object selected = null, bool excludeUnselected = false, string unselectedText = "[ Не выбрано ]")
		{
			var result = new List<SelectListItem>();
			var nameFunc = nameExpr.Compile();
			var valFunc = valueExpr.Compile();

			if (!excludeUnselected)
				result.Add(new SelectListItem { Text = unselectedText, Value = string.Empty, Selected = selected == null });

			result.AddRange(source.Select(item => new SelectListItem
													{
														Text = nameFunc(item), 
														Value = valFunc(item), 
														Selected = (selected != null && selected.ToString() == valFunc(item))
													}));

			return result;
		}

		public static MvcHtmlString SelectFor<TModel, TValue>(this OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expr, IEnumerable<SelectListItem> items, string labelText = null, InputSize size = InputSize.Default, string helpText = null)
		{
			var select = new TagBuilder("select");

			InputExtensions.MergeNameAndId(@select, helper.HtmlHelper, expr);
			FillSelect(select, items, size);

			return helper.InputFor(expr, select.ToString(), labelText, helpText);
		}

		public static MvcHtmlString Select<TModel>(this OdinHelper<TModel> helper, string name, IEnumerable<SelectListItem> items, string labelText = null, string onChange = null, InputSize size = InputSize.Default, string helpText = null)
		{
			var select = new TagBuilder("select");

			select.MergeAttribute("name", name);
			select.MergeAttribute("id", name);
			if (!string.IsNullOrEmpty(onChange))
				select.MergeAttribute("onchange", onChange);
			FillSelect(select, items, size);

			return helper.Input(name, select.ToString(), labelText, helpText);
		}

		public static MvcHtmlString SimpleSelect<TModel>(this OdinHelper<TModel> helper, string name, IEnumerable<SelectListItem> items, string onChange = null, InputSize size = InputSize.Default)
		{
			var select = new TagBuilder("select");

			select.MergeAttribute("name", name);
			select.MergeAttribute("id", name);
			if (!string.IsNullOrEmpty(onChange))
				select.MergeAttribute("onchange", onChange);

			FillSelect(select, items, size);

			return new MvcHtmlString(select.ToString());
		}

		private static void FillSelect(TagBuilder select, IEnumerable<SelectListItem> items, InputSize size)
		{
			if (size != InputSize.Default)
				select.AddCssClass("input-" + size.ToString());

			var sb = new StringBuilder();
			foreach (var item in items)
				sb.AppendLine(ListItemToOption(item));

			select.InnerHtml = sb.ToString();
		}

		private static string ListItemToOption(SelectListItem item)
		{
			TagBuilder tagBuilder = new TagBuilder("option")
										{
											InnerHtml = HttpUtility.HtmlEncode(item.Text)
										};
			if (item.Value != null)
				tagBuilder.Attributes["value"] = item.Value;
			if (item.Selected)
				tagBuilder.Attributes["selected"] = "selected";
			return tagBuilder.ToString(TagRenderMode.Normal);
		}
	}
}