namespace NotLimited.Framework.Web.Controls.Attributes
{
	public class PathPrefixAttribute : MetadataAttribute
	{
		public const string PathPrefixKey = "PathPrefix";

		public PathPrefixAttribute(string pathPrefix)
		{
			PathPrefix = pathPrefix;
		}

		public string PathPrefix { get; set; }
	}
}