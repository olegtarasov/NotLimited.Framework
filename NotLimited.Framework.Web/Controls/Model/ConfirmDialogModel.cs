namespace NotLimited.Framework.Web.Controls.Model
{
	public class ConfirmDialogModel
	{
		public ConfirmDialogModel(string form, string title, string text)
            : this(form, title, text, "confirmDialog")
		{
		}

	    public ConfirmDialogModel(string formId, string title, string text, string dialogId)
	    {
	        FormId = formId;
	        Title = title;
	        Text = text;
	        DialogId = dialogId;
	    }

	    public string FormId { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
	    public string DialogId { get; set; }
	}
}