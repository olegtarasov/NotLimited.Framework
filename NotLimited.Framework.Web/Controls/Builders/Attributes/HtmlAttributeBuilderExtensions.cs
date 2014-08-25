using System.Collections.Generic;
using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls.Builders.Attributes
{
	public static class HtmlAttributeBuilderExtensions
	{
		public static HtmlAttributeBuilder HtmlAttributes<T>(this OdinHelper<T> helper)
		{
			return new HtmlAttributeBuilder();
		}

		public static HtmlAttributeBuilder Size(this HtmlAttributeBuilder builder, InputSize size)
		{
			if (size != InputSize.Default && size != InputSize.Block)
				builder.AddCssClass("input-" + size.ToString());
			if (size == InputSize.Block)
				builder.AddCssClass("input-block-level");

			return builder;
		}
	}
}