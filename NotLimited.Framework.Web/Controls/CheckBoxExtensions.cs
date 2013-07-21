using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NotLimited.Framework.Web.Controls
{
	public static class CheckBoxExtensions
	{
		public static MvcHtmlString CheckBoxFor<TModel>(this OdinHelper<TModel> helper, Expression<Func<TModel, bool>> expr, string label = null, bool isChecked = false)
		{
			return helper.InputFor(expr, helper.HtmlHelper.CheckBoxFor(expr).ToString(), label);
		}

		public static MvcHtmlString CheckBox(this OdinHelper helper, string name, string label = null, bool isChecked = false)
		{
			return helper.Input(name, helper.HtmlHelper.CheckBox(name, isChecked).ToString(), label);
		}

		public static MvcHtmlString DataCheckBox(this OdinHelper helper, string name, string label, bool isChecked = false, object value = null)
		{
			var labelTag = new TagBuilder("label");
			var checkBox = new TagBuilder("input");

			checkBox.MergeAttribute("type", "checkbox");
			checkBox.MergeAttribute("name", name);
			checkBox.SetInnerText(label);

			if (value != null)
				checkBox.MergeAttribute("value", value.ToString());

			if (isChecked)
				checkBox.MergeAttribute("checked", "checked");

			labelTag.AddCssClass("checkbox");
			labelTag.InnerHtml = checkBox.ToString();

			return new MvcHtmlString(labelTag.ToString());
		}
	}
}