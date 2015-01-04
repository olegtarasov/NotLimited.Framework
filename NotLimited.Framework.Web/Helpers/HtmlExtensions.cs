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
        /// <summary>
        /// Concatenates two anonymous objects into a single attrubte dictionary.
        /// </summary>
        public static IDictionary<string, object> ConcatHtmlAttributes(this object attributes, object moreAttributes)
	    {
	        var dic = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
            dic.AddRange(HtmlHelper.AnonymousObjectToHtmlAttributes(moreAttributes));

	        return dic;
	    }

        /// <summary>
        /// Renders anonymous object into HTML attrubte string like 'foo="bar" baz="fuzz"'.
        /// </summary>
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
                if (!string.IsNullOrEmpty(value))
                {
                    string str = HttpUtility.HtmlAttributeEncode(value);
                    sb.Append(' ').Append(key).Append("=\"").Append(str).Append('"');
                }
	        }

            return new MvcHtmlString(sb.ToString());
	    }
	}
}