using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls.Builders
{
    public abstract class FormControlBuilderBase<TBuilder, TModel, TProperty> : ControlBuilderBase<TBuilder, TModel, TProperty> 
        where TBuilder : FormControlBuilderBase<TBuilder, TModel, TProperty>
    {
        protected FormControlBuilderBase(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) : base(htmlHelper)
        {
            Expression = expression;
            CssClass("form-control");
        }

        public Expression<Func<TModel, TProperty>> Expression { get; private set; }
        
        public override HelperResult GetControlHtml()
        {
            return FormHelpers.TextBox(
                HtmlHelper.LabelFor(Expression),
                GetFormControlHtml(),
                HtmlHelper.ValidationMessageFor(Expression, "", new {@class = "text-danger"}));
        }

        protected abstract MvcHtmlString GetFormControlHtml();
    }
}