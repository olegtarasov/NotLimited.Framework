using System;
using System.Linq.Expressions;
using System.Web.WebPages;
using NotLimited.Framework.Web.Controls.Builders;

namespace NotLimited.Framework.Web.Controls
{
    public static class RadioExtensions
    {
        public static HelperResult RadioGroupFor<TModel, TProperty>(this FormHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return new RadioEnumGroupBuilder<TModel, TProperty>(htmlHelper.HtmlHelper, expression);
        }
    }
}