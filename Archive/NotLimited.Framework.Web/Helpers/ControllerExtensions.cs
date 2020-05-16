using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;

namespace NotLimited.Framework.Web.Helpers
{
	public static class ControllerExtensions
	{
		public static IReadOnlyList<MethodInfo> GetActions(this Type type)
		{
			return type.GetMethods(BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public)
							.Where(x => x.ReturnType.IsAssignableTo<ActionResult>())
							.ToList();
		}
	}
}