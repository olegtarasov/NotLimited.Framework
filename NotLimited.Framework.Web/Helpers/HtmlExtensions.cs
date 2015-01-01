using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Web.Helpers
{
	public static class HtmlExtensions
	{
	    public static IDictionary<string, object> ConcatHtmlAttributes(this object attributes, object moreAttributes)
	    {
	        var dic = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
            dic.AddRange(HtmlHelper.AnonymousObjectToHtmlAttributes(moreAttributes));

	        return dic;
	    }

	    public static MvcHtmlString RenderHtmlAttributes(this HtmlHelper helper, object attributes)
	    {
            if (attributes == null)
                return new MvcHtmlString("");

	        var sb = new StringBuilder();
	        var dic = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);

            if (dic.Count == 0)
                return new MvcHtmlString("");

	        foreach (var attr in dic)
	        {
                string key = attr.Key;
	            string value = Convert.ToString(attr.Value);
                if (!string.Equals(key, "id", StringComparison.Ordinal) || !string.IsNullOrEmpty(value))
                {
                    string str = HttpUtility.HtmlAttributeEncode(value);
                    sb.Append(' ').Append(key).Append("=\"").Append(str).Append('"');
                }
	        }

            return new MvcHtmlString(sb.ToString());
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
	}
}