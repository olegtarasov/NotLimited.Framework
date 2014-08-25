namespace NotLimited.Framework.Web.Controls.Attributes
{
	public class PasswordAttribute : MetadataAttribute
	{
		public const string IsPasswordKey = "IsPassword";

		public PasswordAttribute(bool password = true)
		{
			IsPassword = password;
		}

		public bool IsPassword { get; set; }
	}
}