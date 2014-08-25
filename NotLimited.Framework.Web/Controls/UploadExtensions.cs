using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using NotLimited.Framework.Web.Controls.Builders.Upload;

namespace NotLimited.Framework.Web.Controls
{
	public static class UploadExtensions
	{
		public static IUploadBuilder UploadFor<TModel, TValue>(this OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expr)
		{
			return new UploadBuilder<TModel, TValue>(helper, expr);
		}

		public static MvcHtmlString Upload<TModel>(this OdinHelper<TModel> helper, string name, string labelText = null, string helpText = null)
		{
			var textBox = new TagBuilder("input");

			textBox.MergeAttribute("type", "file");
			textBox.MergeAttribute("name", name);
			textBox.MergeAttribute("id", name);

			return helper.Input(name, textBox.ToString(), labelText, helpText);
		}
	}
}