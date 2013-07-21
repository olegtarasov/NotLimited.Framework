using System.Collections.Generic;
using System.Linq;

namespace NotLimited.Framework.Web.Model.Navigation
{
	public class NavigationGroup
	{
		public NavigationGroup()
		{
		}

		public NavigationGroup(string title, params NavigationItem[] items)
		{
			Title = title;
			Items = items.ToList();
		}

		public string Title { get; set; }
		public List<NavigationItem> Items { get; set; }
	}
}