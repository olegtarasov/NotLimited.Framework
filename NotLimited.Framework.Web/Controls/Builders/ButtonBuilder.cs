using System.Web.Mvc;
using System.Web.WebPages;

namespace NotLimited.Framework.Web.Controls.Builders
{
    public class ButtonBuilder : ControlBuilderBase<ButtonBuilder>
    {
        private ActionButtonType _type = ActionButtonType.@default;
        private ActionButtonSize _size = ActionButtonSize.Default;
        private string _text;
        private string _action, _controller;
        private object _route;
        private string _url;
        private string _onClick;
        private bool _isSubmit;

        public ButtonBuilder(HtmlHelper htmlHelper) : base(htmlHelper)
        {

        }

        public ButtonBuilder Type(ActionButtonType type)
        {
            _type = type;
            return this;
        }

        public ButtonBuilder Size(ActionButtonSize size)
        {
            _size = size;
            return this;
        }

        public ButtonBuilder Text(string text)
        {
            _text = text;
            return this;
        }

        public ButtonBuilder Action(string action, string controller = null)
        {
            _action = action;
            _controller = controller;
            return this;
        }

        public ButtonBuilder Route(object route)
        {
            _route = route;
            return this;
        }

        public ButtonBuilder Url(string url)
        {
            _url = url;
            return this;
        }

        public ButtonBuilder OnClick(string onClick)
        {
            _onClick = onClick;
            return this;
        }

        public ButtonBuilder Submit()
        {
            _isSubmit = true;
            return this;
        }

        public override HelperResult GetControlHtml()
        {
            var link = new TagBuilder(_isSubmit ? "button" : "a");
            link.AddCssClass("btn");
            link.AddCssClass("btn-" + _type.ToString());
            if (_size != ActionButtonSize.Default)
            {
                link.AddCssClass("btn-" + _size.ToString());
            }

            if (!_isSubmit)
            {
                string url = _url;
                if (string.IsNullOrEmpty(url) && (!string.IsNullOrEmpty(_action) || !string.IsNullOrEmpty(_controller)))
                {
                    url = new UrlHelper(HtmlHelper.ViewContext.RequestContext).Action(_action, _controller, _route);
                }

                if (string.IsNullOrEmpty(url))
                {
                    url = "#";
                }

                link.MergeAttribute("href", url);
            }
            else
            {
                link.MergeAttribute("type", "submit");
            }

            if (!string.IsNullOrEmpty(_onClick))
            {
                link.MergeAttribute("onclick", _onClick);
            }

            object id;
            if (HtmlAttributes.TryGetValue("id", out id))
            {
                link.MergeAttribute("id", (string)id);
            }

            link.InnerHtml = _text;

            return new HelperResult(writer => writer.Write(link.ToString()));
        }
    }
}