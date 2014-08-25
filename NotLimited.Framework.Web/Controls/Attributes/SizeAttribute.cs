namespace NotLimited.Framework.Web.Controls.Attributes
{
	public class SizeAttribute : MetadataAttribute
	{
		public const string SizeKey = "Size";

		public SizeAttribute(InputSize size = InputSize.Default)
		{
			Size = size;
		}

		public InputSize Size { get; set; }
	}
}