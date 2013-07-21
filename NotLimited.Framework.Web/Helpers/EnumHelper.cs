using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NotLimited.Framework.Web.Helpers
{
	public static class EnumHelper
	{
        public static T MergeFlags<T>(this IEnumerable<T> source)
        {
            return (T)Enum.ToObject(typeof(T), (int)source.Sum(x => (int)Convert.ChangeType(x, typeof(int))));
        }

		public static Dictionary<T, string> GetEnumDictionary<T>()
		{
			var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);

			return (from field in fields
					let value = field.GetRawConstantValue()
					let attribute = field.GetCustomAttribute<DescriptionAttribute>()
					let description = attribute != null ? attribute.Description : field.Name
					orderby value
					select new { Name = description, Value = value })
				.ToDictionary(x => (T)x.Value, x => x.Name);
		}

		public static string GetEnumValueString<T>(this T en)
		{
			return GetEnumDictionary<T>()[en];
		}
	}
}