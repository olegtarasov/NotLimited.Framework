using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using NotLimited.Framework.Web.Controls.Builders;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class TextBoxExtensions
	{
	    /// <summary>
	    /// Creates a text box.
	    /// </summary>
	    public static TextBoxBuilder<TModel, TProperty> TextBoxFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
	    {
            return new TextBoxBuilder<TModel, TProperty>(helper.HtmlHelper, expression);
	    }

	    /// <summary>
	    /// Creates a text area.
	    /// </summary>
        public static TextAreaBuilder<TModel, TProperty> TextAreaFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
	    {
	        return new TextAreaBuilder<TModel, TProperty>(helper.HtmlHelper, expression);
	    }

	    /// <summary>
	    /// Creates a password field.
	    /// </summary>
        public static TextBoxBuilder<TModel, TProperty> PasswordFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
	    {
	        return new TextBoxBuilder<TModel, TProperty>(helper.HtmlHelper, expression)
                .Type(TextBoxType.Password);
	    }

        /// <summary>
        /// Creates a readonly field.
        /// </summary>
        public static TextBoxBuilder<TModel, TProperty> ReadonlyFor<TModel, TProperty>(this FormHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return new TextBoxBuilder<TModel, TProperty>(helper.HtmlHelper, expression)
                .Type(TextBoxType.Readonly);
        }
	}
}