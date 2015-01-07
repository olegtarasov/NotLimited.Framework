using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Web.Controls.Builders
{
    public abstract class ControlBuilderBase<TBuilder> where TBuilder : ControlBuilderBase<TBuilder>
    {
        protected readonly HtmlHelper HtmlHelper;

        protected ControlBuilderBase(HtmlHelper htmlHelper)
        {
            if (htmlHelper == null) throw new ArgumentNullException("htmlHelper");

            HtmlHelper = htmlHelper;
            CssClasses = new HashSet<string>();
            HtmlAttributes = new Dictionary<string, object>();
        }

        public HashSet<string> CssClasses { get; private set; }
        public Dictionary<string, object> HtmlAttributes { get; private set; }

        public TBuilder CssClass(string css)
        {
            CssClasses.Add(css);
            return (TBuilder)this;
        }

        public TBuilder HtmlAttribute(string name, string value)
        {
            HtmlAttributes[name] = value;
            return (TBuilder)this;
        }

        public TBuilder Id(string id)
        {
            HtmlAttributes["id"] = id;
            return (TBuilder)this;
        }

        public static implicit operator HelperResult(ControlBuilderBase<TBuilder> builder)
        {
            return builder.GetControlHtml();
        }

        public abstract HelperResult GetControlHtml();

        protected string GetCssClassString()
        {
            return CssClasses.Aggregate((s, s1) => s + " " + s1);
        }

        protected Dictionary<string, object> GetAttributesDictionary()
        {
            object attrClass;
            if (HtmlAttributes.TryGetValue("class", out attrClass))
            {
                string strClass = (string)attrClass;
                var classes = strClass.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (classes.Length > 0)
                {
                    CssClasses.AddRange(classes);
                }

                HtmlAttributes.Remove("class");
            }

            var result = new Dictionary<string, object>(HtmlAttributes);
            result["class"] = GetCssClassString();

            return result;
        }
    }

    public abstract class ControlBuilderBase<TBuilder, TModel, TProperty> : ControlBuilderBase<TBuilder> 
        where TBuilder : ControlBuilderBase<TBuilder, TModel, TProperty>
    {
        protected new readonly HtmlHelper<TModel> HtmlHelper;

        protected ControlBuilderBase(HtmlHelper<TModel> htmlHelper) : base(htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        public static implicit operator HelperResult(ControlBuilderBase<TBuilder, TModel, TProperty> builder)
        {
            return builder.GetControlHtml();
        }
    }
}