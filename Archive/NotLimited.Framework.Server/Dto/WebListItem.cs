namespace NotLimited.Framework.Server.Dto
{
	/// <summary>
	/// SelectListItem's counterpart.
	/// </summary>
	public class WebListItem
	{
		public string Text { get; set; }
		public string Value { get; set; }
		public bool Selected { get; set; }
		public bool Disabled { get; set; } 
	}
}