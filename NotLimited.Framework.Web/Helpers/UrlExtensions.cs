using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Web.Helpers
{
    public static class UrlExtensions
    {
        /// <summary>
        /// Generates an Async version of the current Url.
        /// Suppose current url is '/Users/Show/1?foo=bar'. Resulting Url will be '/Users/ShowAsync/1?foo=bar'.
        /// </summary>
        /// <param name="helper">Url helper</param>
        /// <param name="action">Action name override</param>
        /// <param name="controller">Controller name override</param>
        public static string AsyncLoadUrl(this UrlHelper helper, string action = null, string controller = null)
        {
            var result = new RouteValueDictionary();

            foreach (var value in helper.RequestContext.RouteData.Values)
                result.Add(value.Key, value.Value);

            if (!String.IsNullOrEmpty(controller))
                result["controller"] = controller;
			
            result["action"] = !String.IsNullOrEmpty(action) ? action : (string)result["action"] + "Async";

            return helper.RouteUrl(result);
        }

        /// <summary>
        /// Returns an absolute Url for server-relative url.
        /// </summary>
        public static string Absolute(this UrlHelper helper, string url)
        {
            if (url.StartsWithOrdinal("http://", true))
            {
                return url;
            }

            var originalUrl = HttpContext.Current.Request.Url;
            
            return originalUrl.Scheme + "://" + originalUrl.Authority + url;
        }
    }
}