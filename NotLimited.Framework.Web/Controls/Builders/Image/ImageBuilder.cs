using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using NotLimited.Framework.Web.Controls.Attributes;

namespace NotLimited.Framework.Web.Controls.Builders.Image
{
	public class ImageBuilder<TModel, TValue> : InputBuilderBase<TModel, TValue, IImageBuilder>, IImageBuilder
	{
		public ImageBuilder(OdinHelper<TModel> helper, Expression<Func<TModel, TValue>> expression) : base(helper, expression)
		{
			if (_metadata.HasAdditionalValue(PathPrefixAttribute.PathPrefixKey))
				PathPrefix(_metadata.GetAdditionalValue<string>(PathPrefixAttribute.PathPrefixKey));
		}

		private string _pathPrefix;
		private string _altText;

		public IImageBuilder PathPrefix(string path)
		{
			_pathPrefix = path;
			return this;
		}

		public IImageBuilder AltText(string text)
		{
			_altText = text;
			return this;
		}

		public override MvcHtmlString GetControlHtml()
		{
			var builder = new TagBuilder("img");

			builder.MergeAttribute("src", (!string.IsNullOrEmpty(_pathPrefix) ? _pathPrefix : "") + _metadata.Model.ToString());
			if (!string.IsNullOrEmpty(_altText))
				builder.MergeAttribute("alt", _altText);

			return new MvcHtmlString(builder.ToString());
		}
	}
}