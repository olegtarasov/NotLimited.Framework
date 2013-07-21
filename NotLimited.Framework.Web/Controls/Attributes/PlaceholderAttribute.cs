namespace NotLimited.Framework.Web.Controls.Attributes
{
	public class PlaceholderAttribute : MetadataAttribute
	{
		public const string PlaceholderKey = "Placeholder";

		public PlaceholderAttribute(string placeholder = null)
		{
			Placeholder = placeholder;
		}

		public string Placeholder { get; set; }
	}
}