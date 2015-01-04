using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;
using NotLimited.Framework.Web.Helpers;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class ButtonExtensions
	{
		public static MvcHtmlString ActionButton(this FormHelper helper, string text, string actionName = null, string controllerName = null, ActionButtonType type = ActionButtonType.@default, ActionButtonSize size = ActionButtonSize.Default, object routeValues = null, object htmlAttributes = null)
		{
			var dic = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			dic.Add("class", "btn btn-" + type.ToString() + (size != ActionButtonSize.Default ? " btn-" + size.ToString() : ""));

			return helper.HtmlHelper.ActionLink(text, actionName, controllerName, routeValues.ToRouteValueDictionary(), dic);
		}

		public static MvcHtmlString CancelButton(this FormHelper helper, string text, ActionButtonType type = ActionButtonType.@default, ActionButtonSize size = ActionButtonSize.Default)
		{
			return ActionButton(helper, text, helper.HtmlHelper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer, type, size);
		}

	    public static MvcHtmlString GoBackLink(this FormHelper helper, string text, string action, string controller)
	    {
	        var builder = new TagBuilder("a");
            builder.MergeAttribute("href", helper.HtmlHelper.GetReferrerUrl(action, controller));
            builder.SetInnerText(text);

	        return new MvcHtmlString(builder.ToString());
	    }

		public static MvcHtmlString ActionButton(this FormHelper helper, string text, Uri uri, ActionButtonType type = ActionButtonType.@default, ActionButtonSize size = ActionButtonSize.Default)
		{
			var link = new TagBuilder("a");

			link.AddCssClass("btn");
			if (type != ActionButtonType.@default)
				link.AddCssClass("btn-" + type.ToString());
			if (size != ActionButtonSize.Default)
				link.AddCssClass("btn-" + size.ToString());

			if (uri != null)
				link.MergeAttribute("href", uri.ToString());
			link.SetInnerText(text);

			return new MvcHtmlString(link.ToString());
		}

		public static MvcHtmlString Button(this FormHelper helper, string text, string handler = null, ActionButtonType type = ActionButtonType.@default, ActionButtonSize size = ActionButtonSize.Default, object htmlAttributes = null)
		{
			var button = new TagBuilder("a");

			button.AddCssClass("btn");
			if (type != ActionButtonType.@default)
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

		public static MvcHtmlString SubmitWithConfirmButton(this FormHelper helper, string text, string formId, string title, string message, ActionButtonSize size = ActionButtonSize.Default, ActionButtonType type = ActionButtonType.primary)
		{
			var builder = new StringBuilder(Button(helper, text, "ConfirmDialog();", type, size).ToString());
			builder.AppendLine().Append(helper.ConfirmDialog(formId, title, message));

			return new MvcHtmlString(builder.ToString());
		}

		public static MvcHtmlString SubmitButton(this FormHelper helper, string text, ActionButtonSize size = ActionButtonSize.Default, ActionButtonType type = ActionButtonType.primary)
		{
			var input = new TagBuilder("input");

			input.AddCssClass("btn");
			if (type != ActionButtonType.@default)
				input.AddCssClass("btn-" + type.ToString());
			if (size != ActionButtonSize.Default)
				input.AddCssClass("btn-" + size.ToString());
			input.MergeAttribute("type", "submit");
			input.MergeAttribute("value", text);

			return new MvcHtmlString(input.ToString());
		}

	    /// <summary>
	    /// Creates a submit button.
	    /// </summary>
	    public static HelperResult SubmitButton<TModel>(this FormHelper<TModel> helper, string text = "Сохранить")
	    {
	        return FormHelpers.SubmitButton(text);
	    }
	}
}