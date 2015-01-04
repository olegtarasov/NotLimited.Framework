using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls
{
    public class FormHelper<T> : FormHelper
    {
        public FormHelper(HtmlHelper<T> htmlHelper) : base(htmlHelper)
        {
        }

        public new HtmlHelper<T> HtmlHelper { get { return (HtmlHelper<T>)base.HtmlHelper; }}
    }

    public class FormHelper
    {
        public FormHelper(HtmlHelper htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        public HtmlHelper HtmlHelper { get; private set; }
    }
}