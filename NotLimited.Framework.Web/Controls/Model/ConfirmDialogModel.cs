namespace NotLimited.Framework.Web.Controls.Model
{
	public class ConfirmDialogModel
	{
		public ConfirmDialogModel(string form, string title, string text)
		{
			FormId = form;
			Title = title;
			Text = text;
		}

		public string FormId { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
	}
}