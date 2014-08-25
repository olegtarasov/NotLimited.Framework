using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NotLimited.Framework.Web.Controls
{
	public static class HiddenExtensions
	{
		public static MvcHtmlString HiddenReturn(this OdinHelper helper)
		{
			return helper.HtmlHelper.Hidden("ReturnUrl", helper.HtmlHelper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer);
		}
	}
}