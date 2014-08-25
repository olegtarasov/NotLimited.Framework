namespace NotLimited.Framework.Web.Navigation
{
	public enum NavigationItemType
	{
		Item,
		Group
	}

	public abstract class NavigationItemBase
	{
		protected NavigationItemBase(string title, string icon = null)
		{
			Title = title;
			Icon = icon;
		}

		public string Title { get; set; }
		public string Icon { get; set; }

		public abstract NavigationItemType Type { get; }
	}
}