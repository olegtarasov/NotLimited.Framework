using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using NotLimited.Framework.Web.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class FormExtensions
	{
		public static MvcForm BeginHorizontalForm(this HtmlHelper htmlHelper, string action = null, string controller = null, object htmlAttributes = null)
		{
			var attributes = htmlAttributes == null ? new Dictionary<string, object>() : htmlAttributes.ToDictionary();
			attributes.Add("class", "form-horizontal");

			return htmlHelper.BeginForm(action, controller, FormMethod.Post, attributes);
		}

		 public static MvcForm BeginForm(this HtmlHelper htmlHelper, FormMethod method, object htmlAttributes)
		 {
			 return htmlHelper.BeginForm(null, null, method, htmlAttributes);
		 }

		 public static MvcForm BeginUploadForm(this HtmlHelper htmlHelper, string action = null, string controller = null)
		 {
			 return htmlHelper.BeginForm(action, controller, FormMethod.Post, new { enctype = "multipart/form-data", @class = "form-horizontal" });
		 }
	}
}