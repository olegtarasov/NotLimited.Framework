using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.WebPages;
using NotLimited.Framework.Data.Queries;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls.Grid
{
    public static class FilterHelpers
    {
        public static HelperResult FilterFor<TModel, TKey>(System.Web.Mvc.HtmlHelper helper, Expression<Func<TModel, TKey>> expression, IEnumerable<System.Web.Mvc.SelectListItem> items, string placeholder)
        {
            var prop = PropertyMetadataCache<TModel>.GetPropertyMetadata(expression);
            if (!prop.Filterable)
                throw new InvalidOperationException("Property is not filterable!");

            return FilterViewHelper.FilterBox(helper, prop.PropertyInfo.Name, prop.Description, items, placeholder);
        }
    }
}