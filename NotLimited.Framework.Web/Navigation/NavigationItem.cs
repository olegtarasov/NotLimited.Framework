using System;
using System.Linq.Expressions;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Web.Navigation
{
	public class NavigationItem : NavigationItemBase
	{
		public NavigationItem(string title, string icon = null, string action = null, string controller = null) : base(title, icon)
		{
			Action = action;
			Controller = controller;
		}

		public static NavigationItem From<TModel, TKey>(Expression<Func<TModel, TKey>> expr, string title, string icon)
		{
			string controllerName = expr.GetTypeName();
			int pos = controllerName.LastIndexOf("Controller", StringComparison.OrdinalIgnoreCase);
			if (pos > -1)
				controllerName = controllerName.Substring(0, pos);

			return new NavigationItem(title, icon, expr.GetMemberName(), controllerName);
		}

		public string Action { get; set; }
		public string Controller { get; set; }

		public override NavigationItemType Type
		{
			get { return NavigationItemType.Item; }
		}
	}
}