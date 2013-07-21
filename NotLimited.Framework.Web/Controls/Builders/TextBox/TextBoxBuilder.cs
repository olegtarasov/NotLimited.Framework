using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using NotLimited.Framework.Web.Controls.Attributes;
using NotLimited.Framework.Web.Controls.Builders.Attributes;

namespace NotLimited.Framework.Web.Controls.Builders.TextBox
{
	public class TextBoxBuilder<TModel, TValue> : InputBuilderBase<TModel, TValue, ITextBoxBuilder>, ITextBoxBuilder
	{
		public TextBoxBuilder(OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expression) : base(helper, expression)
		{
			if (_metadata.AdditionalValues.ContainsKey(PlaceholderAttribute.PlaceholderKey))
				Placeholder((string)_metadata.AdditionalValues[PlaceholderAttribute.PlaceholderKey]);

			if (_metadata.AdditionalValues.ContainsKey(PasswordAttribute.IsPasswordKey))
				Password((bool)_metadata.AdditionalValues[PasswordAttribute.IsPasswordKey]);
		}

		public bool HasPlaceholder { get; set; }
		public string PlaceholderText { get; set; }

		public bool IsPassword { get; set; }

		public ITextBoxBuilder Password(bool password = true)
		{
			IsPassword = password;
			return this;
		}

		public ITextBoxBuilder Placeholder(string text = null)
		{
			HasPlaceholder = true;
			PlaceholderText = GetLabelText(text);

			return this;
		}

		public override MvcHtmlString GetControlHtml()
		{
			_attributes.Size(ControlSize);

			if (HasPlaceholder)
				_attributes.SetAttrubte("placeholder", PlaceholderText);

			return IsPassword
				       ? _helper.HtmlHelper.PasswordFor(_expression, _attributes.ToAttributeDictionary())
				       : _helper.HtmlHelper.TextBoxFor(_expression, _attributes.ToAttributeDictionary());
		}
	}
}