namespace NotLimited.Framework.Web.Controls.Builders.TextArea
{
	public interface ITextAreaBuilder : IInputBuilder<ITextAreaBuilder>
	{
		ITextAreaBuilder Rows(int rows);
	}
}