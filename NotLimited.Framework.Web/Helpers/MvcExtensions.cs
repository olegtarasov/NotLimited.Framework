using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Routing;

namespace NotLimited.Framework.Web.Helpers
{
	public static class MvcExtensions
	{
		public static Dictionary<string, object> ToDictionary(this object value)
		{
			return TypeDescriptor.GetProperties(value).Cast<PropertyDescriptor>().ToDictionary(property => property.Name.Replace('_', '-'), property => property.GetValue(value));
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