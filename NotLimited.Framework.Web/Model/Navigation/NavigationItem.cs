namespace NotLimited.Framework.Web.Model.Navigation
{
    public class NavigationItem
    {
    	public NavigationItem()
    	{
    	}

    	public NavigationItem(string text, string controller, string action, long? id = null, string icon = null)
	    {
		    Text = text;
		    Controller = controller;
		    Action = action;
		    Id = id;
    		Icon = icon;
	    }

	    public string Text { get; set; }
    	public string Controller { get; set; }
    	public string Action { get; set; }
	    public string Icon { get; set; }
	    public long? Id { get; set; }
    }
}
