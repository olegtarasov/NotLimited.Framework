using System;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NotLimited.Framework.Web.Controls.Builders
{
    public class TextAreaBuilder<TModel, TProperty> : FormControlBuilderBase<TextAreaBuilder<TModel, TProperty>, TModel, TProperty>
    {
        private int _rows = 5, _columns = 75;

        public TextAreaBuilder(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) : base(htmlHelper, expression)
        {
        }

        public TextAreaBuilder<TModel, TProperty> Rows(int rows)
        {
            _rows = rows;
            return this;
        }

        public TextAreaBuilder<TModel, TProperty> Columns(int columns)
        {
            _columns = columns;
            return this;
        }

        protected override MvcHtmlString GetFormControlHtml()
        {
            return HtmlHelper.TextAreaFor(Expression, _rows, _columns, GetAttributesDictionary());
        }
    }
}