using System.Web.Mvc;
using System.Web.WebPages;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
    public static class FontAwesomeExtensions
    {
        public static HelperResult Icon(this HtmlHelper helper, string icon)
        {
            return IconHelpers.SimpleIcon(icon);
        }

        public static HelperResult IconText(this HtmlHelper helper, string icon, string text)
        {
            return IconHelpers.IconText(icon, text);
        }

        public static HelperResult IconMargin(this HtmlHelper helper, string icon)
        {
            return IconHelpers.IconMargin(icon);
        }
    }
}