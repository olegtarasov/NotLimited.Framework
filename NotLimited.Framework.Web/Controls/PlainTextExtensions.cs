using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls
{
	public static class PlainTextExtensions
	{
		public static MvcHtmlString PlainText<TModel>(this OdinHelper<TModel> helper, string name, string value, string labelText = null, InputSize size = InputSize.Default, string helpText = null)
		{
			var div = new TagBuilder("div");

			div.AddCssClass("form-field-plaintext");
			div.InnerHtml = value;

			return helper.Input(name, div.ToString(), labelText, helpText);
		}
	}
}