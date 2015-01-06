using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NotLimited.Framework.Web.Controls.Builders
{
    public enum SelectControlType
    {
        DropDown,
        ListBox,
        EnumDropDown
    }

    public class SelectBuilder<TModel, TProperty> : FormControlBuilderBase<SelectBuilder<TModel, TProperty>, TModel, TProperty>
    {
        private SelectControlType _type = SelectControlType.DropDown;
        private IEnumerable<SelectListItem> _items;

        public SelectBuilder(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) : base(htmlHelper, expression)
        {
        }

        public SelectBuilder<TModel, TProperty> Type(SelectControlType type)
        {
            _type = type;
            return this;
        }

        public SelectBuilder<TModel, TProperty> Select2(string placeholder = null, bool allowClear = false)
        {
            CssClasses.Add("select2");

            if (!string.IsNullOrEmpty(placeholder))
            {
                HtmlAttributes.Add("data-placeholder", placeholder);
            }

            if (allowClear)
            {
                HtmlAttributes.Add("data-allowClear", "true");
            }

            return this;
        }

        public SelectBuilder<TModel, TProperty> OnChange(string handler)
        {
            HtmlAttributes["onchange"] = handler;
            return this;
        }

        public SelectBuilder<TModel, TProperty> Items(IEnumerable<SelectListItem> items)
        {
            _items = items;
            return this;
        }

        protected override MvcHtmlString GetFormControlHtml()
        {
            switch (_type)
            {
                case SelectControlType.DropDown:
                    return HtmlHelper.DropDownListFor(Expression, _items, GetAttributesDictionary());
                case SelectControlType.ListBox:
                    return HtmlHelper.ListBoxFor(Expression, _items, GetAttributesDictionary());
                case SelectControlType.EnumDropDown:
                    return HtmlHelper.EnumDropDownListFor(Expression, GetAttributesDictionary());
            }

            throw new InvalidOperationException("Unsupported select type!");
        }
    }
}