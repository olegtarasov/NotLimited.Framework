namespace NotLimited.Framework.Web.Controls.Builders.TextBox
{
	public interface ITextBoxBuilder : IInputBuilder<ITextBoxBuilder>
	{
		ITextBoxBuilder Password(bool password = true);
		ITextBoxBuilder Placeholder(string text = null);
	}
}