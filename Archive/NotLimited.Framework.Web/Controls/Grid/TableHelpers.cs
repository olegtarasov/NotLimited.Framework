using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.WebPages;
using NotLimited.Framework.Common.Helpers;
using NotLimited.Framework.Server.Queries;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls.Grid
{
    public static class TableHelpers
    {
        public enum SortOrder
        {
            None,
            Ascending,
            Descending
        }
        
        public static HelperResult TableHeader<TModel, TKey>(System.Web.Mvc.HtmlHelper helper, Expression<Func<TModel, TKey>> expression, string title)
        {
            var metadata = PropertyMetadataCache<TModel>.GetPropertyMetadata(expression);
            string memberName = expression.GetMemberName();
            return GridViewHelper.TableHeader(helper, metadata, title ?? metadata.DisplayName, GetSortOrder(memberName));
        }

        public static HelperResult TableHeader<TModel, TKey>(System.Web.Mvc.HtmlHelper helper, Expression<Func<TModel, TKey>> expression)
        {
            return TableHeader(helper, expression, null);
        }

        public static SortOrder GetSortOrder(string sortMember)
        {
            var quesryString = HttpContext.Current.Request.QueryString;

            if (quesryString["sortBy"] != sortMember)
                return SortOrder.None;

            return (quesryString["descending"] == null || String.Equals(quesryString["descending"], "false", StringComparison.OrdinalIgnoreCase))
                       ? SortOrder.Ascending
                       : SortOrder.Descending;
        }

        public static string GetSortIcon(SortOrder order)
        {
            if (order == SortOrder.None)
                return "fa-sort";
            if (order == SortOrder.Ascending)
                return "fa-sort-asc";
            if (order == SortOrder.Descending)
                return "fa-sort-desc";

            return null;
        }

        public static bool IsOppositeOrderDescending(SortOrder order)
        {
            if (order == SortOrder.None || order == SortOrder.Descending)
                return false;
        
            return true;
        }
    }
}