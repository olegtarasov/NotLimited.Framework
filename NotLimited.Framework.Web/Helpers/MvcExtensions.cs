using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Routing;

namespace NotLimited.Framework.Web.Helpers
{
	public static class MvcExtensions
	{
        /// <summary>
        /// Convert an anonymous object to dictionary.
        /// </summary>
        public static IDictionary<string, object> ToDictionary(this object obj)
	    {
	        return HtmlHelper.AnonymousObjectToHtmlAttributes(obj);
	    }

        /// <summary>
        /// Convert an anonymous object to RouteValueDictionary.
        /// </summary>
        public static RouteValueDictionary ToRouteValueDictionary(this object value)
		{
			var result = new RouteValueDictionary();

			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value))
				result.Add(property.Name, property.GetValue(value));

			return result;
		}
	}
}