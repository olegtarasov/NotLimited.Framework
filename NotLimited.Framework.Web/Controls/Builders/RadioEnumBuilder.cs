using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NotLimited.Framework.Web.Controls.Builders
{
    public class RadioEnumBuilder<TModel, TProperty> : FormControlBuilderBase<RadioEnumBuilder<TModel, TProperty>, TModel, TProperty>
    {
        private readonly object _value;

        public RadioEnumBuilder(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value) : base(htmlHelper, expression)
        {
            _value = value;
        }

        protected override MvcHtmlString GetFormControlHtml()
        {
            return HtmlHelper.RadioButtonFor(Expression, _value);
        }
    }
}