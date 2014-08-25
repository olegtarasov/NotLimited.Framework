using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls.Builders.Upload
{
	public class UploadBuilder<TModel, TValue> : InputBuilderBase<TModel, TValue, IUploadBuilder>, IUploadBuilder
	{
		public UploadBuilder(OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expression) : base(helper, expression)
		{
		}

		public override MvcHtmlString GetControlHtml()
		{
			var builder = new TagBuilder("input");

			builder.MergeAttribute("type", "file");
			builder.MergeNameAndId(_helper.HtmlHelper, _expression);

			return new MvcHtmlString(builder.ToString());
		}
	}
}