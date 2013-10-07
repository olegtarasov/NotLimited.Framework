namespace NotLimited.Framework.Web.Controls.Attributes
{
	public class RowsAttribute : MetadataAttribute
	{
		public const string RowsKey = "Rows";

		public RowsAttribute(int rows)
		{
			Rows = rows;
		}

		public int Rows { get; set; }
	}
}