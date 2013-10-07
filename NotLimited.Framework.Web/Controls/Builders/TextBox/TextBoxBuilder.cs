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
			if (_metadata.HasAdditionalValue(PlaceholderAttribute.PlaceholderKey))
				Placeholder(_metadata.GetAdditionalValue<string>(PlaceholderAttribute.PlaceholderKey));

			if (_metadata.HasAdditionalValue(PasswordAttribute.IsPasswordKey))
				Password(_metadata.GetAdditionalValue<bool>(PasswordAttribute.IsPasswordKey));
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
			_attributes.SetAttrubte("placeholder", text);

			return this;
		}

		public override MvcHtmlString GetControlHtml()
		{
			return IsPassword
				       ? _helper.HtmlHelper.PasswordFor(_expression, _attributes.ToAttributeDictionary())
				       : _helper.HtmlHelper.TextBoxFor(_expression, _attributes.ToAttributeDictionary());
		}
	}
}