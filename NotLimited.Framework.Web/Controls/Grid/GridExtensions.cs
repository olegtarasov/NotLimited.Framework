using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;
using NotLimited.Framework.Data.Queries;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls.Grid
{
    public static class GridExtensions
    {
        public static GridBuilder<T> Grid<T>(this HtmlHelper helper, IEnumerable<T> models, Pagination pagination = null, HashSet<string> fields = null, object tableHtmlAttributes = null)
        {
            return new GridBuilder<T>(helper, models, pagination, tableHtmlAttributes, fields);
        }

        public static HelperResult GridScript(this HtmlHelper helper)
        {
            return GridViewHelper.GridScript();
        }
    }
}