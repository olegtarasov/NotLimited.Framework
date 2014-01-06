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

		public string Action { get; set; }
		public string Controller { get; set; }

		public override NavigationItemType Type
		{
			get { return NavigationItemType.Item; }
		}
	}
}