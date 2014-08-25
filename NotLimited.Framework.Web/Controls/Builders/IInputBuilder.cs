using NotLimited.Framework.Web.Controls.Builders.TextBox;

namespace NotLimited.Framework.Web.Controls.Builders
{
	public interface IInputBuilder<T> : IBuilder where T : IBuilder
	{
		T Size(InputSize size);
		T Label(string text = null);
	}
}