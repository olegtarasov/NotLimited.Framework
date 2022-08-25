using System;
using System.Reflection;

namespace NotLimited.Framework.Common.Testing;

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
				throw new InvalidOperationException($"Property {prop.Name} is not equal between objects!");
			}
		}
	}
}