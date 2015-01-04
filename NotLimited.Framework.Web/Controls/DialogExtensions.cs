using System.Web.Mvc;
using System.Web.Mvc.Html;
using NotLimited.Framework.Web.Controls.Model;

namespace NotLimited.Framework.Web.Controls
{
	public static class DialogExtensions
	{
		 public static MvcHtmlString ConfirmDialog(this FormHelper helper, string formId, string title, string message)
		 {
			 return helper.HtmlHelper.Partial("ConfirmDialog", new ConfirmDialogModel(formId, title, message));
		 }
	}
}