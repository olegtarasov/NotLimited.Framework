using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace NotLimited.Framework.Common.Helpers
{
	public static class EnumHelper
	{
		public static T MergeFlags<T>(this IEnumerable<T> source)
		{
			return (T)Enum.ToObject(typeof(T), source.Sum(x => (int)Convert.ChangeType(x, typeof(int))));
		}

		public static Dictionary<T, string> GetEnumDictionary<T>()
		{
			return GetEnumDictionary<T>(typeof(T));
		}

		public static Dictionary<T, string> GetEnumDictionary<T>(Type enumType)
		{
			var fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

			return (from field in fields
					let value = field.GetRawConstantValue()
					let description = field.GetDisplayName()
					orderby value
					select new { Name = description, Value = value })
				.ToDictionary(x => (T)x.Value, x => x.Name);
		}

		public static string GetEnumValueString<T>(this T en)
		{
			return GetEnumDictionary<T>()[en];
		}

	    public static string GetDisplayName(this FieldInfo field)
	    {
	        var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
	        if (displayAttr != null && !string.IsNullOrEmpty(displayAttr.Name))
	            return displayAttr.Name;

	        var descAttr = field.GetCustomAttribute<DescriptionAttribute>();
	        if (descAttr != null && !string.IsNullOrEmpty(descAttr.Description))
	            return descAttr.Description;

	        return field.Name;
	    }
	}
}