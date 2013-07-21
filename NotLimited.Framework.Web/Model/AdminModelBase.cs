using System.Collections.Generic;
using NotLimited.Framework.Web.Model.Navigation;

namespace NotLimited.Framework.Web.Model
{
	public abstract class AdminModelBase
	{
		protected AdminModelBase()
		{
			NavigationGroups = new List<NavigationGroup>();
		}

		public List<NavigationGroup> NavigationGroups { get; set; } 
	}
}