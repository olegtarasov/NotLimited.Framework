using System.Web.Mvc;
using System.Web.Mvc.Html;
using NotLimited.Framework.Web.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class HiddenExtensions
	{
        public static MvcHtmlString HiddenCurrentUrl(this OdinHelper helper)
        {
            return helper.HtmlHelper.Hidden("ReturnUrl", helper.HtmlHelper.GetCurrentUrl());
        }

        public static MvcHtmlString HiddenReferrer(this OdinHelper helper, string action, string controller)
        {
            return helper.HtmlHelper.Hidden("ReturnUrl", helper.HtmlHelper.GetReferrerUrl(action, controller));
        }

        public static MvcHtmlString HiddenReferrer(this OdinHelper helper)
		{
			return helper.HtmlHelper.Hidden("ReturnUrl", helper.HtmlHelper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer);
		}
	}
}