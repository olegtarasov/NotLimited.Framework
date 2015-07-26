using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;
using NotLimited.Framework.Web.Controls.Builders;
using NotLimited.Framework.Web.Helpers;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
	public static class ButtonExtensions
	{
        /// <summary>
        /// Creates a button that leads to an action.
        /// </summary>
        public static ButtonBuilder ActionButton(this FormHelper helper, string text, string action = null, string controller = null)
		{
			return new ButtonBuilder(helper.HtmlHelper)
                .Text(text)
                .Action(action, controller);
		}

        /// <summary>
        /// Creates a button that goes back to referrer url or to default action.
        /// </summary>
        public static ButtonBuilder GoBackButton(this FormHelper helper, string text, string defaultAction = null, string defaultController = null)
		{
		    string url = null;
		    if (helper.HtmlHelper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer != null)
		    {
                url = helper.HtmlHelper.ViewContext.RequestContext.HttpContext.Request.UrlReferrer.PathAndQuery;
		    }
		    
		    return new ButtonBuilder(helper.HtmlHelper)
                .Text(text)
                .Url(url)
                .Action(defaultAction, defaultController);
		}

        /// <summary>
        /// Creates a link that goes back to referrer url or to default action.
        /// </summary>
	    public static MvcHtmlString GoBackLink(this FormHelper helper, string text, string action, string controller)
	    {
	        var builder = new TagBuilder("a");
            builder.MergeAttribute("href", helper.HtmlHelper.GetReferrerUrl(action, controller));
            builder.InnerHtml = text;

	        return new MvcHtmlString(builder.ToString());
	    }

        /// <summary>
        /// Creates a button with text.
        /// </summary>
        public static ButtonBuilder Button(this FormHelper helper, string text)
		{
			return new ButtonBuilder(helper.HtmlHelper)
                .Text(text);
		}

		/// <summary>
	    /// Creates a submit button.
	    /// </summary>
        public static ButtonBuilder SubmitButton<TModel>(this FormHelper<TModel> helper, string text = "Сохранить")
	    {
	        return new ButtonBuilder(helper.HtmlHelper)
                .Submit()
                .Text(text)
                .Type(ActionButtonType.primary);
	    }
	}
}