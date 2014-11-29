using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.WebPages;
using NotLimited.Framework.Data.Queries;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls.Grid
{
    public static class GridHelpers
    {
        public static HelperResult Grid<T>(this HtmlHelper helper, IEnumerable<T> models, Expression<Func<T, object>>[] expressions, Pagination pagination = null, HashSet<string> fields = null, object tableHtmlAttributes = null)
        {
            var headers = expressions.Select(expression => TableHelpers.TableHeader(helper, expression, fields)).ToList();
            var rows = new List<List<HelperResult>>();

            foreach (var model in models)
            {
                rows.Add(expressions.Select(x => TableHelpers.ModelFieldTableView(helper, model, fields, x)).ToList());
            }


            return GridViewHelper.Grid(helper, headers, rows, pagination, tableHtmlAttributes);
        }
    }
}