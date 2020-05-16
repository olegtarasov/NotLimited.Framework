using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NotLimited.Framework.Server.Dto;

namespace NotLimited.Framework.Server.Helpers
{
	public static class WebListHelpers
	{
		/// <summary>
		/// Transfoms a collection into a list of WebListItems.
		/// </summary>
		/// <param name="source">Source collection</param>
		/// <param name="nameExpr">Function that extracts a title from an element.</param>
		/// <param name="valueExpr">Function that extracts a value from an element.</param>
		/// <param name="selected">Selected value.</param>
		/// <param name="includeUnselected">Include and "Unselected" item.</param>
		/// <param name="unselectedText">"Unselected" item text.</param>
		/// <param name="enabledValues">Values that are enabled in a box.</param>
		public static List<WebListItem> ToSelectListItems<T>(this IEnumerable<T> source, Expression<Func<T, string>> nameExpr, Expression<Func<T, string>> valueExpr, object selected = null, bool includeUnselected = false, string unselectedText = "[ Не выбрано ]", HashSet<string> enabledValues = null)
		{
			var result = new List<WebListItem>();
			var nameFunc = nameExpr.Compile();
			var valFunc = valueExpr.Compile();

			if (includeUnselected)
			{
				result.Add(new WebListItem { Text = unselectedText, Value = string.Empty, Selected = selected == null });
			}

			result.AddRange(source.Select(item =>
			{
				var value = valFunc(item);
				return new WebListItem
				{
					Text = nameFunc(item),
					Value = value,
					Selected = (selected != null && selected.ToString() == valFunc(item)),
					Disabled = enabledValues != null && !enabledValues.Contains(value)
				};
			}));

			return result;
		}
	}
}