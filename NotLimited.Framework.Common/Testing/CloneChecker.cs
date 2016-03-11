using System;
using System.Reflection;

namespace NotLimited.Framework.Common.Testing
{
	public static class CloneChecker
	{
		public static void AssertMembersEqualTo<T>(this T a, T b)
		{
			var props = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

			foreach (var prop in props)
			{
				var valA = prop.GetValue(a);
				var valB = prop.GetValue(b);

				if (!object.Equals(valA, valB))
				{
					throw new InvalidOperationException(string.Format("Property {0} is not equal between objects!", prop.Name));
				}
			}
		}
	}
}