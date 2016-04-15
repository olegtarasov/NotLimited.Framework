using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NotLimited.Framework.Common.Helpers
{
	public static class EnumHelper
	{
		private static Dictionary<Type, Dictionary<string, int>> _valueMap = new Dictionary<Type, Dictionary<string, int>>();

		public static T FromDoubleArray<T>(this double[] array)
		{
			var values = GetEnumValues<T>().Select(x => x.Value).OrderBy(x => x).ToList();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] > 0)
				{
					return (T)(object)values[i];
				}
			}

			return default(T);
		}

		public static double[] ToDoubleArray<T>(this T en)
		{
			var values = GetEnumValues<T>().Select(x => x.Value).OrderBy(x => x).ToList();
			var result = new double[values.Count];
			result[values.IndexOf(Convert.ToInt32(en))] = 1;

			return result;
		}

		public static T MergeFlags<T>(this IEnumerable<T> source)
		{
			return (T)Enum.ToObject(typeof(T), source.Sum(x => (int)Convert.ChangeType(x, typeof(int))));
		}

		public static Dictionary<string, int> GetEnumValues<T>()
		{
			var type = typeof(T);
			Dictionary<string, int> result;
			if (!_valueMap.TryGetValue(type, out result))
			{
				result = GetEnumDictionaryReflection<T>();
				_valueMap[type] = result;
			}

			return result;
		}

		[Obsolete]
		public static Dictionary<T, string> GetEnumDictionary<T>()
		{
			return GetEnumDictionary<T>(typeof(T));
		}

		[Obsolete]
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

		public static Dictionary<string, int> GetEnumDictionaryReflection<T>()
		{
			var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

			var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

			return (from field in fields
					let value = field.GetRawConstantValue()
					let description = field.GetDisplayName()
					orderby value
					select new { Name = description, Value = value })
				.ToDictionary(x => x.Name, x => (int)x.Value);
		}
	}
}