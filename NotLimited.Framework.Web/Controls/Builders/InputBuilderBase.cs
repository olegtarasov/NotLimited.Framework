using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using NotLimited.Framework.Web.Controls.Attributes;
using NotLimited.Framework.Web.Controls.Builders.Attributes;
using NotLimited.Framework.Web.Controls.Builders.TextBox;

namespace NotLimited.Framework.Web.Controls.Builders
{
	public abstract class InputBuilderBase<TModel, TValue, TBuilder> : HtmlBuilderBase, IBuilder, IInputBuilder<TBuilder> where TBuilder : IBuilder
	{
		protected readonly OdinHelper<TModel> _helper;
		protected readonly Expression<Func<TModel, TValue>> _expression;

		protected readonly string _name;
		protected readonly ModelMetadata _metadata;

		protected readonly HtmlAttributeBuilder _attributes = new HtmlAttributeBuilder();

		protected InputBuilderBase(OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
		{
			_expression = expression;
			_helper = helper;

			_name = helper.HtmlHelper.GetControlName(expression);
			_metadata = ModelMetadata.FromLambdaExpression(expression, helper.HtmlHelper.ViewData);

			if (_metadata.HasAdditionalValue(SizeAttribute.SizeKey))
				Size(_metadata.GetAdditionalValue<InputSize>(SizeAttribute.SizeKey));

			if (_metadata.HasAdditionalValue(LabelAttribute.LabelKey))
				Label(_metadata.GetAdditionalValue<string>(LabelAttribute.LabelKey));
		}

		public string LabelText { get; set; }
		public bool HasLabel { get; set; }

		public string HelpText { get; set; }
		public bool HasHelp { get; set; }

		public InputSize ControlSize { get; set; }

		public TBuilder Size(InputSize size)
		{
			ControlSize = size;
			_attributes.Size(size);

			return (TBuilder)(object)this;
		}

		public TBuilder Label(string text = null)
		{
			HasLabel = true;
			LabelText = GetLabelText(text);

			return (TBuilder)(object)this;
		}

		protected string GetLabelText(string text)
		{
			return string.IsNullOrEmpty(text)
					   ? string.IsNullOrEmpty(_metadata.DisplayName)
							 ? _name
							 : _metadata.DisplayName
					   : text;
		}

		public abstract MvcHtmlString GetControlHtml();

		protected override MvcHtmlString GetHtmlString()
		{
			if (!HasLabel && !HasHelp)
				return GetControlHtml();

			var clear = new TagBuilder("div");
			var input = new TagBuilder("div");

			clear.AddCssClass("form-group");
			input.AddCssClass("controls");
			input.InnerHtml = GetControlHtml().ToString();

			if (HasLabel)
			{
				var label = new TagBuilder("label");
				label.AddCssClass("control-label");
				label.MergeAttribute("for", _name);
				label.SetInnerText(string.IsNullOrEmpty(LabelText) ? _name : LabelText);

				clear.InnerHtml += label.ToString();
			}

			if (HasHelp && !string.IsNullOrEmpty(HelpText))
			{
				var help = new TagBuilder("span");
				help.AddCssClass("help-block");
				help.InnerHtml = HelpText;
				
				input.InnerHtml += help.ToString();
			}

			clear.InnerHtml += input.ToString();

			return new MvcHtmlString(clear.ToString());
		}
	}
}