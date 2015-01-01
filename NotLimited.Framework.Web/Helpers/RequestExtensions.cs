using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NotLimited.Framework.Web.Helpers
{
    public static class RequestExtensions
    {
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

        public static bool IsRouteMatch(this Uri uri, string controllerName, string actionName)
        {
            var routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
            return (routeInfo.RouteData.Values["controller"].ToString() == controllerName && routeInfo.RouteData.Values["action"].ToString() == actionName);
        }

        public static string GetRouteParameterValue(this Uri uri, string paramaterName)
        {
            var routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
            return routeInfo.RouteData.Values[paramaterName] != null ? routeInfo.RouteData.Values[paramaterName].ToString() : null;
        }

        public static RouteValueDictionary QueryStringToRouteDictionary(this HtmlHelper helper)
        {
            var result = new RouteValueDictionary();

            foreach (var key in helper.ViewContext.HttpContext.Request.QueryString.AllKeys)
                result[key] = helper.ViewContext.HttpContext.Request.QueryString[key];

            return result;
        }

        public static MvcHtmlString AppendQueryStringJson<TModel>(this HtmlHelper<TModel> helper, object routeValues)
        {
            var result = AppendQueryString(helper, routeValues);
            string res = ToJson(result);
			
            return new MvcHtmlString(res);
        }

        public static RouteValueDictionary AppendQueryString<TModel>(this HtmlHelper<TModel> helper, object routeValues)
        {
            var result = new RouteValueDictionary();

            foreach (var key in helper.ViewContext.HttpContext.Request.QueryString.AllKeys)
                result[key] = helper.ViewContext.HttpContext.Request.QueryString[key];

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(routeValues))
                result[property.Name] = property.GetValue(routeValues);

            return result;
        }

        public static RouteValueDictionary GetUrlRoute(this Uri url)
        {
            string query = url == null ? null : url.Query;
            var route = new RouteValueDictionary();

            // TODO: Implement injection protection!
            if (!string.IsNullOrEmpty(query)/* && url.IsRouteMatch(controller, action)*/)
            {
                HttpUtility.ParseQueryString(query).CopyTo(route);
            }

            return route;
        }

        public static RouteValueDictionary GetReferrerRoute(this HtmlHelper helper)
        {
            return helper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer.GetUrlRoute();
        }

        public static RouteValueDictionary GetCurrentUrlRoute(this HtmlHelper helper)
        {
            return helper.ViewContext.RequestContext.HttpContext.Request.Url.GetUrlRoute();
        }

        public static string GetCurrentUrl(this HtmlHelper helper)
        {
            return helper.ViewContext.RequestContext.HttpContext.Request.Url.PathAndQuery;
        }

        public static string GetReferrerUrl(this HtmlHelper helper, string action, string controller)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            return urlHelper.Action(action, controller, helper.GetReferrerRoute());
        }

        private static string ToJson(this RouteValueDictionary dictionary)
        {
            var builder = new StringBuilder("{");

            foreach (var pair in dictionary)
                builder.Append('"').Append((string)pair.Key).Append("\" : \"").Append((object)pair.Value).Append("\", ");

            builder.Remove(builder.Length - 2, 2);
            builder.Append('}');

            return builder.ToString();
        }
    }
}