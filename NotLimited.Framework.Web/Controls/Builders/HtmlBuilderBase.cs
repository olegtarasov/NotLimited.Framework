using System.Web;
using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls.Builders
{
	public abstract class HtmlBuilderBase : IHtmlString
	{
		protected abstract MvcHtmlString GetHtmlString();

		public string ToHtmlString()
		{
			return GetHtmlString().ToString();
		}
	}
}