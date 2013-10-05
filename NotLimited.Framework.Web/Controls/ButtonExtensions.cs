using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using NotLimited.Framework.Web.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class ButtonExtensions
	{
		public static MvcHtmlString ActionButton(this OdinHelper helper, string text, string actionName = null, string controllerName = null, ActionButtonType type = ActionButtonType.Default, ActionButtonSize size = ActionButtonSize.Default, object routeValues = null, object htmlAttributes = null)
		{
			var dic = htmlAttributes.ToDictionary();

			dic.Add("class", "btn" + (type != ActionButtonType.Default ? " btn-" + type.ToString() : "") + (size != ActionButtonSize.Default ? " btn-" + size.ToString() : ""));

			return helper.HtmlHelper.ActionLink(text, actionName, controllerName, routeValues.ToRouteValueDictionary(), dic);
		}

		public static MvcHtmlString CancelButton(this OdinHelper helper, string text, ActionButtonType type = ActionButtonType.Default, ActionButtonSize size = ActionButtonSize.Default)
		{
			return ActionButton(helper, text, helper.HtmlHelper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer, type, size);
		}

		public static MvcHtmlString ActionButton(this OdinHelper helper, string text, Uri uri, ActionButtonType type = ActionButtonType.Default, ActionButtonSize size = ActionButtonSize.Default)
		{
			var link = new TagBuilder("a");

			link.AddCssClass("btn");
			if (type != ActionButtonType.Default)
				link.AddCssClass("btn-" + type.ToString());
			if (size != ActionButtonSize.Default)
				link.AddCssClass("btn-" + size.ToString());

			if (uri != null)
				link.MergeAttribute("href", uri.ToString());
			link.SetInnerText(text);

			return new MvcHtmlString(link.ToString());
		}

		public static MvcHtmlString Button(this OdinHelper helper, string text, string handler = null, ActionButtonType type = ActionButtonType.Default, ActionButtonSize size = ActionButtonSize.Default, object htmlAttributes = null)
		{
			var button = new TagBuilder("a");

			button.AddCssClass("btn");
			if (type != ActionButtonType.Default)
				button.AddCssClass("btn-" + type.ToString());
			if (size != ActionButtonSize.Default)
				button.AddCssClass("btn-" + size.ToString());
			if (htmlAttributes != null)
				button.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

			if (!String.IsNullOrEmpty(handler))
				button.MergeAttribute("onclick", handler);
			button.SetInnerText(text);

			return new MvcHtmlString(button.ToString());
		}

		public static MvcHtmlString SubmitWithConfirmButton(this OdinHelper helper, string text, string formId, string title, string message, ActionButtonSize size = ActionButtonSize.Default, ActionButtonType type = ActionButtonType.primary)
		{
			var builder = new StringBuilder(Button(helper, text, "ConfirmDialog();", type, size).ToString());
			builder.AppendLine().Append((object)helper.ConfirmDialog(formId, title, message));

			return new MvcHtmlString(builder.ToString());
		}

		public static MvcHtmlString SubmitButton(this OdinHelper helper, string text, ActionButtonSize size = ActionButtonSize.Default, ActionButtonType type = ActionButtonType.primary)
		{
			var input = new TagBuilder("input");

			input.AddCssClass("btn");
			if (type != ActionButtonType.Default)
				input.AddCssClass("btn-" + type.ToString());
			if (size != ActionButtonSize.Default)
				input.AddCssClass("btn-" + size.ToString());
			input.MergeAttribute("type", "submit");
			input.MergeAttribute("value", text);

			return new MvcHtmlString(input.ToString());
		}
	}
}