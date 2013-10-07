using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using NotLimited.Framework.Web.Controls.Attributes;
using NotLimited.Framework.Web.Controls.Builders.Attributes;

namespace NotLimited.Framework.Web.Controls.Builders.TextArea
{
	public class TextAreaBuilder<TModel, TValue> : InputBuilderBase<TModel, TValue, ITextAreaBuilder>, ITextAreaBuilder
	{
		public TextAreaBuilder(OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expression) : base(helper, expression)
		{
			if (_metadata.HasAdditionalValue(RowsAttribute.RowsKey))
				Rows(_metadata.GetAdditionalValue<int>(RowsAttribute.RowsKey));
		}

		private int _rows = 0;

		public override MvcHtmlString GetControlHtml()
		{
			var builder = new TagBuilder("textarea");
			builder.MergeNameAndId(_helper.HtmlHelper, _expression);
			builder.MergeAttributes(_attributes.ToAttributeDictionary(), true);
			if (_metadata.Model != null)
				builder.SetInnerText(_metadata.Model.ToString());
			
			return new MvcHtmlString(builder.ToString());
		}

		public ITextAreaBuilder Rows(int rows)
		{
			_rows = rows;
			_attributes.SetAttrubte("rows", rows.ToString());
			return this;
		}
	}
}