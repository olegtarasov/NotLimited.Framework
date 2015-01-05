using System.Collections.Generic;
using System.Web.Mvc;
using NotLimited.Framework.Data.Queries;

namespace NotLimited.Framework.Web.Controls.Grid
{
    public static class GridHelpers
    {
        public static GridBuilder<T> Grid<T>(this HtmlHelper helper, IEnumerable<T> models, Pagination pagination = null, HashSet<string> fields = null, object tableHtmlAttributes = null)
        {
            return new GridBuilder<T>(helper, models, pagination, tableHtmlAttributes, fields);
        }
    }
}