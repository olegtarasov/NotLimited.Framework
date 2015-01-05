using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;

namespace NotLimited.Framework.Web.Controls.Builders
{
    public enum TextBoxType
    {
        Default,
        Password
    }

    public class TextBoxBuilder<TModel, TProperty> : FormControlBuilderBase<TextBoxBuilder<TModel, TProperty>, TModel, TProperty>
    {
        private TextBoxType _type = TextBoxType.Default;

        public TextBoxBuilder(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) : base(htmlHelper, expression)
        {
        }

        public TextBoxBuilder<TModel, TProperty> Type(TextBoxType type)
        {
            _type = type;
            return this;
        }

        protected override MvcHtmlString GetFormControlHtml()
        {
            switch (_type)
            {
                case TextBoxType.Default:
                    return HtmlHelper.TextBoxFor(Expression, GetAttributesDictionary());
                case TextBoxType.Password:
                    return HtmlHelper.PasswordFor(Expression, GetAttributesDictionary());
            }

            throw new InvalidOperationException("Invalid text box type!");
        }
    }
}