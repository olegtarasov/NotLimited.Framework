using System.Collections.Generic;

namespace NotLimited.Framework.Web.Navigation
{
	public class NavigationGroup : NavigationItemBase
	{
		public NavigationGroup(string title, string icon = null, List<NavigationItemBase> children = null) : base(title, icon)
		{
			Children = children ?? new List<NavigationItemBase>();
		}

		public List<NavigationItemBase> Children { get; private set; }

		public override NavigationItemType Type
		{
			get { return NavigationItemType.Group; }
		}
	}
}