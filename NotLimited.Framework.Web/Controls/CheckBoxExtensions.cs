using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Properties;
using NotLimited.Framework.Web.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class CheckBoxExtensions
	{
		public static MvcHtmlString DataCheckBox(this FormHelper helper, string name, string label, bool isChecked = false, object value = null)
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

	    public static MvcHtmlString SimpleCheckBox(this FormHelper htmlHelper, string name, object htmlAttributes)
	    {
            string result = htmlHelper.HtmlHelper.CheckBox(name, htmlAttributes).ToString();
            return new MvcHtmlString(result.Substring(0, result.IndexOf("<input", 5)));
	    }

		public static MvcHtmlString SimpleCheckBoxFor<TModel>(this FormHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
		{
			// Well, this is a stinking hack.
			string result = htmlHelper.HtmlHelper.CheckBoxFor(expression, htmlAttributes).ToString();
			return new MvcHtmlString(result.Substring(0, result.IndexOf("<input", 5)));
		}
	}
}