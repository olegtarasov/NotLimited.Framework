using System.Web.Mvc;
using System.Web.WebPages;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
    public static class FontAwesomeExtensions
    {
        /// <summary>
        /// Creates a simple icon.
        /// </summary>
        public static HelperResult Icon(this HtmlHelper helper, string icon)
        {
            return IconHelpers.SimpleIcon(icon);
        }

        /// <summary>
        /// Creates an icon followed by an 8 px margin and text.
        /// </summary>
        public static HelperResult IconText(this HtmlHelper helper, string icon, string text)
        {
            return IconHelpers.IconText(icon, text);
        }

        /// <summary>
        /// Creates an icon follewed by an 8 px margin.
        /// </summary>
        public static HelperResult IconMargin(this HtmlHelper helper, string icon)
        {
            return IconHelpers.IconMargin(icon);
        }
    }
}