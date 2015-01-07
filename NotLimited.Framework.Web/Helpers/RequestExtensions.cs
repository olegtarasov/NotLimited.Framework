using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Web.Helpers
{
    public static class RequestExtensions
    {
        /// <summary>
        /// Gets the base part of the Url, i. e. 'http://foo.bar:785' from 'http://foo.bar:785/baz?fuzz=tits'.
        /// </summary>
        public static string GetBaseUrl(this HttpRequestBase request)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (request.Url == null) throw new ArgumentException("request.Url");

            string scheme = request.Url.GetLeftPart(UriPartial.Scheme);
            if (String.IsNullOrEmpty(scheme))
                throw new InvalidOperationException();

            string url = scheme + request.Url.Host;
            if (request.Url.Port != 80)
                url += ":" + request.Url.Port;

            return url;
        }

        /// <summary>
        /// Indicates whether supplied Url matches specific controller and action.
        /// </summary>
        public static bool IsRouteMatch(this Uri url, string actionName, string controllerName)
        {
            var routeInfo = new RouteInfo(url, HttpContext.Current.Request.ApplicationPath);
            return (routeInfo.RouteData.Values["controller"].ToString() == controllerName && routeInfo.RouteData.Values["action"].ToString() == actionName);
        }

        /// <summary>
        /// Extracts specified GET parameter from an Url.
        /// </summary>
        public static string GetRouteParameterValue(this Uri uri, string paramaterName)
        {
            var routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
            return routeInfo.RouteData.Values[paramaterName] != null ? routeInfo.RouteData.Values[paramaterName].ToString() : null;
        }

        /// <summary>
        /// Converts current GET parameters to a RouteValueDictionary.
        /// </summary>
        public static RouteValueDictionary QueryStringToRouteDictionary(this HtmlHelper helper, params string[] except)
        {
            var result = new RouteValueDictionary();
            var set = new HashSet<string>(except, StringComparer.OrdinalIgnoreCase);

            foreach (var key in helper.ViewContext.HttpContext.Request.QueryString.AllKeys)
            {
                if (String.IsNullOrEmpty(key) || set.Contains(key))
                    continue;

                result[key] = helper.ViewContext.HttpContext.Request.QueryString[key];
            }

            return result;
        }

        /// <summary>
        /// Appends GET parameters from an anonymous object to current set of GET parameters
        /// and returns the result as JSON string.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="helper"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static MvcHtmlString AppendQueryStringJson<TModel>(this HtmlHelper<TModel> helper, object routeValues)
        {
            var result = AppendQueryString(helper, routeValues);
            string res = ToJson(result);
			
            return new MvcHtmlString(res);
        }

        /// <summary>
        /// Appends GET parameters from an anonymous object to current set of GET parameters.
        /// </summary>
        public static RouteValueDictionary AppendQueryString(this HtmlHelper helper, object routeValues)
        {
            var result = new RouteValueDictionary();

            foreach (var key in helper.ViewContext.HttpContext.Request.QueryString.AllKeys)
            {
                if (String.IsNullOrEmpty(key))
                    continue;

                result[key] = helper.ViewContext.HttpContext.Request.QueryString[key];
            }

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(routeValues))
                result[property.Name] = property.GetValue(routeValues);

            return result;
        }

        /// <summary>
        /// Gets GET query params from specified Url in a form of a RouteValueDictionary.
        /// </summary>
        public static RouteValueDictionary GetUrlQueryParams(this Uri url)
        {
            string query = url == null ? null : url.Query;
            var route = new RouteValueDictionary();

            // TODO: Implement injection protection!
            if (!String.IsNullOrEmpty(query)/* && url.IsRouteMatch(controller, action)*/)
            {
                HttpUtility.ParseQueryString(query).CopyTo(route);
            }

            return route;
        }

        /// <summary>
        /// Gets GET query params from current Refferer url in a form of a RouteValueDictionary.
        /// </summary>
        public static RouteValueDictionary GetReferrerQueryParams(this HtmlHelper helper)
        {
            return helper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer.GetUrlQueryParams();
        }

        /// <summary>
        /// Gets a relative part of the current request Url.
        /// </summary>
        public static string GetCurrentUrl(this HtmlHelper helper)
        {
            return helper.ViewContext.RequestContext.HttpContext.Request.Url != null 
                ? helper.ViewContext.RequestContext.HttpContext.Request.Url.PathAndQuery 
                : String.Empty;
        }

        /// <summary>
        /// Constructs an url from action and contoller with referrer GET params.
        /// </summary>
        public static string GetReferrerUrl(this HtmlHelper helper, string action, string controller)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            return urlHelper.Action(action, controller, helper.GetReferrerQueryParams());
        }

        /// <summary>
        /// Converts a RouteValueDictionary to JSON string.
        /// </summary>
        public static string ToJson(this RouteValueDictionary dictionary)
        {
            var builder = new StringBuilder("{");

            foreach (var pair in dictionary)
                builder.Append('"').Append(pair.Key).Append("\" : \"").Append(pair.Value).Append("\", ");

            builder.Remove(builder.Length - 2, 2);
            builder.Append('}');

            return builder.ToString();
        }

        public static RouteValueDictionary ConcatRouteValues(this object source, RouteValueDictionary dst)
        {
            var result = new RouteValueDictionary(source);
            result.AddRange(dst);

            return result;
        }

        /// <summary>
        /// Convert an anonymous object to RouteValueDictionary.
        /// </summary>
        public static RouteValueDictionary ToRouteValueDictionary(this object value)
        {
            var result = new RouteValueDictionary();
            if (value == null)
                return result;

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value))
                result.Add(property.Name, property.GetValue(value));

            return result;
        }
    }
}