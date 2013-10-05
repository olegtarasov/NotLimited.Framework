namespace NotLimited.Framework.Web.Controls.Builders.Image
{
	public interface IImageBuilder : IInputBuilder<IImageBuilder>
	{
		IImageBuilder PathPrefix(string path);
		IImageBuilder AltText(string text);
	}
}