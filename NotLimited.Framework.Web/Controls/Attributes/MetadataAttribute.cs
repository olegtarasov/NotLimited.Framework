using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NotLimited.Framework.Web.Controls.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public abstract class MetadataAttribute : Attribute
	{
		private static readonly Dictionary<Type, List<PropertyInfo>> _propertyMap = new Dictionary<Type, List<PropertyInfo>>();

		public IEnumerable<KeyValuePair<string, object>> ToKeyValuePairs()
		{
			return GetProperties()
				.Select(property => new KeyValuePair<string, object>(property.Name, property.GetValue(this, null)));
		}

		private List<PropertyInfo> GetProperties()
		{
			List<PropertyInfo> result;
			var type = GetType();

			if (_propertyMap.ContainsKey(type))
				result = _propertyMap[type];
			else
			{
				result = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToList();
				_propertyMap[type] = result;
			}

			return result;
		}
	}
}