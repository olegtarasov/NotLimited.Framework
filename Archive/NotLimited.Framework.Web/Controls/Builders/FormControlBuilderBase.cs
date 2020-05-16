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
        private ButtonBuilder _buttonBuilder;
        private string _label;

        protected FormControlBuilderBase(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) : base(htmlHelper)
        {
            Expression = expression;
            CssClass("form-control");
        }

        public Expression<Func<TModel, TProperty>> Expression { get; private set; }

        public TBuilder WithButton(Action<ButtonBuilder> builder)
        {
            if (builder == null) throw new ArgumentNullException("builder");

            _buttonBuilder = new ButtonBuilder(HtmlHelper);
            builder(_buttonBuilder);
            return (TBuilder)this;
        }


        public TBuilder Label(string label)
        {
            _label = label;
            return (TBuilder)this;
        }

        public override HelperResult GetControlHtml()
        {
            return FormHelpers.TextBox(
                !string.IsNullOrEmpty(_label) ? HtmlHelper.LabelFor(Expression, _label) : HtmlHelper.LabelFor(Expression),
                _buttonBuilder == null 
                    ? GetFormControlHtml()
                    : new MvcHtmlString(FormHelpers.InputWithButton(GetFormControlHtml(), new MvcHtmlString(_buttonBuilder.GetControlHtml().ToHtmlString())).ToHtmlString()),
                HtmlHelper.ValidationMessageFor(Expression, "", new {@class = "text-danger"}));
        }

        protected abstract MvcHtmlString GetFormControlHtml();
    }
}