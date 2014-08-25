using System;
using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls
{
	public static class NavigationExtensions
	{
		public static MvcHtmlString NavigationItem<TModel>(this OdinHelper<TModel> helper, string text, string action, string controller)
		{
			if (string.IsNullOrEmpty(action)) throw new ArgumentNullException("action");
			if (string.IsNullOrEmpty(controller)) throw new ArgumentNullException("controller");

			var li = new TagBuilder("li");
			var a = new TagBuilder("a");

			if (controller.Equals((string)helper.HtmlHelper.ViewContext.RouteData.Values["controller"], StringComparison.OrdinalIgnoreCase)
			    && action.Equals((string)helper.HtmlHelper.ViewContext.RouteData.Values["action"], StringComparison.OrdinalIgnoreCase))
			{
				li.AddCssClass("active");	
			}

			a.MergeAttribute("href", new UrlHelper(helper.HtmlHelper.ViewContext.RequestContext).Action(action, controller));
			a.InnerHtml = text;

			li.InnerHtml = a.ToString();

			return new MvcHtmlString(li.ToString());
		}
	}
}