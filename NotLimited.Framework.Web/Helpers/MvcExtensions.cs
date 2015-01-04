using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Routing;

namespace NotLimited.Framework.Web.Helpers
{
	public static class MvcExtensions
	{
	    public static IDictionary<string, object> ToDictionary(this object obj)
	    {
	        return HtmlHelper.AnonymousObjectToHtmlAttributes(obj);
	    }

	    public static RouteValueDictionary ToRouteValueDictionary(this object value)
		{
			var result = new RouteValueDictionary();

			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value))
				result.Add(property.Name, property.GetValue(value));

			return result;
		}
	}
}