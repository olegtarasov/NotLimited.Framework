using System;
using System.Web.Mvc;
using System.Web.WebPages;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls.Builders
{
    public class DialogBuilder : ControlBuilderBase<DialogBuilder>
    {
        private readonly string _id;

        private Func<IDisposable> _form;
        private string _okText = "OK";
        private string _cancelText = "Отмена";
        private Func<object, HelperResult> _body;
        private string _okHandler;
        private string _title;

        public DialogBuilder(HtmlHelper htmlHelper, string id) : base(htmlHelper)
        {
            if (id == null) throw new ArgumentNullException("id");

            _id = id;
        }

        public DialogBuilder Form(Func<IDisposable> form)
        {
            _form = form;
            return this;
        }

        public DialogBuilder Title(string title)
        {
            _title = title;
            return this;
        }

        public DialogBuilder OkText(string text)
        {
            _okText = text;
            return this;
        }

        public DialogBuilder CancelText(string text)
        {
            _cancelText = text;
            return this;
        }

        public DialogBuilder OkHandler(string handler)
        {
            _okHandler = handler;
            return this;
        }

        public DialogBuilder Body(Func<dynamic, HelperResult> body)
        {
            _body = body;
            return this;
        }

        public override HelperResult GetControlHtml()
        {
            return DialogHelpers.ModalDialog(_id, _title, _body, _form, _okHandler, _okText, _cancelText);
        }
    }
}