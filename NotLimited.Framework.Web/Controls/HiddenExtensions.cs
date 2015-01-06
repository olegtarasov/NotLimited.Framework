using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using NotLimited.Framework.Web.Helpers;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class HiddenExtensions
	{
        public static MvcHtmlString HiddenCurrentUrl(this FormHelper helper)
        {
            return helper.HtmlHelper.Hidden("ReturnUrl", helper.HtmlHelper.GetCurrentUrl());
        }

        public static MvcHtmlString HiddenReferrer(this FormHelper helper, string action, string controller)
        {
            return helper.HtmlHelper.Hidden("ReturnUrl", helper.HtmlHelper.GetReferrerUrl(action, controller));
        }

        public static MvcHtmlString HiddenReferrer(this FormHelper helper)
        {
            string url = null;
            if (helper.HtmlHelper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer != null)
            {
                url = helper.HtmlHelper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer.PathAndQuery;
            }

			return helper.HtmlHelper.Hidden("ReturnUrl", url);
		}

        /// <summary>
        /// Generates hidden form fields for specified GET query parameters.
        /// </summary>
        public static HelperResult HiddenForQuery(this FormHelper helper, params string[] except)
	    {
	        return FilterViewHelper.HiddenForQuery(helper.HtmlHelper, new HashSet<string>(except, StringComparer.OrdinalIgnoreCase));
	    }
	}
}