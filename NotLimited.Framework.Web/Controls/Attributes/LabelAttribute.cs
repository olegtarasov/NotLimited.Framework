namespace NotLimited.Framework.Web.Controls.Attributes
{
	public class LabelAttribute : MetadataAttribute
	{
		public const string LabelKey = "Label";

		public LabelAttribute(string label = null)
		{
			Label = label;
		}

		public string Label { get; set; }
	}
}