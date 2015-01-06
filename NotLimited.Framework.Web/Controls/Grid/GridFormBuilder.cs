using System;
using System.Threading;
using System.Web.Mvc;
using System.Web.WebPages;

namespace NotLimited.Framework.Web.Controls.Grid
{
    public class GridFormBuilder
    {
        private FormMethod _method = FormMethod.Post;
        private string _action;
        private string _controller;
        private string _id;
        private Func<object, HelperResult> _template;

        public GridFormBuilder Method(FormMethod method)
        {
            _method = method;
            return this;
        }

        public GridFormBuilder Action(string action, string controller = null)
        {
            _action = action;
            _controller = controller;
            return this;
        }

        public GridFormBuilder Id(string id)
        {
            _id = id;
            return this;
        }

        public GridFormBuilder Controls(Func<dynamic, HelperResult> template)
        {
            _template = template;
            return this;
        }

        public FormMethod FormMethod { get { return _method; } }
        public string FormAction { get { return _action; } }
        public string FormController { get { return _controller; } }
        public string FormId { get { return _id; } }
        public Func<object, HelperResult> FormControls { get { return _template; }}
    }
}