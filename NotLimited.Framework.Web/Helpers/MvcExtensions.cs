using System.ComponentModel;
using System.Web.Routing;

namespace NotLimited.Framework.Web.Helpers
{
	public static class MvcExtensions
	{
	    public static RouteValueDictionary ToRouteValueDictionary(this object value)
		{
			var result = new RouteValueDictionary();

			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value))
				result.Add(property.Name, property.GetValue(value));

			return result;
		}
	}
}