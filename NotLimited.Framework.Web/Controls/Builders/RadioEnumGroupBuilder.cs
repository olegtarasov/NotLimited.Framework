using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using NotLimited.Framework.Web.Mvc;

namespace NotLimited.Framework.Web.Controls.Builders
{
    public class RadioEnumGroupBuilder<TModel, TProperty> : FormControlBuilderBase<RadioEnumGroupBuilder<TModel, TProperty>, TModel, TProperty>
    {
        public RadioEnumGroupBuilder(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) : base(htmlHelper, expression)
        {
        }

        protected override MvcHtmlString GetFormControlHtml()
        {
            var func = Expression.Compile();
            var value = func(HtmlHelper.ViewData.Model);
            var enumType = value.GetType();
            var builder = new StringBuilder();


            foreach (var enumValue in Enum.GetValues(enumType))
            {
                builder.Append(HtmlHelper.RadioButtonFor(Expression, enumValue));
            }

            return new MvcHtmlString(builder.ToString());
        }
    }
}