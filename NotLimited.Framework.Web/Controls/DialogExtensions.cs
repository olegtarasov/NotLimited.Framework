using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using NotLimited.Framework.Web.Controls.Builders;
using NotLimited.Framework.Web.Controls.Model;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class DialogExtensions
	{
	    /// <summary>
	    /// Creates a confirmation dialog.
	    /// </summary>
        public static MvcHtmlString ConfirmDialog(this FormHelper helper, string formId, string title, string message)
	    {
	        return helper.HtmlHelper.Partial("ConfirmDialog", new ConfirmDialogModel(formId, title, message));
	    }

        /// <summary>
        /// Creates a modal dialog.
        /// </summary>
        public static DialogBuilder ModalDialog(this FormHelper helper, string id)
	    {
	        return new DialogBuilder(helper.HtmlHelper, id);
	    }
	}
}