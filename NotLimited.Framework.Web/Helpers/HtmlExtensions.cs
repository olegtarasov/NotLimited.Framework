using System.ComponentModel;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace NotLimited.Framework.Web.Helpers
{
	public static class HtmlExtensions
	{
		public static MvcHtmlString AppendQueryStringJson<TModel>(this HtmlHelper<TModel> helper, object routeValues)
		{
			var result = AppendQueryString<TModel>(helper, routeValues);
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

		public static string AsyncLoadUrl(this UrlHelper helper, string action = null, string controller = null)
		{
			var result = new RouteValueDictionary();

			foreach (var value in helper.RequestContext.RouteData.Values)
				result.Add(value.Key, value.Value);

			if (!string.IsNullOrEmpty(controller))
				result["controller"] = controller;
			
			result["action"] = !string.IsNullOrEmpty(action) ? action : (string)result["action"] + "Async";

			return helper.RouteUrl(result);
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