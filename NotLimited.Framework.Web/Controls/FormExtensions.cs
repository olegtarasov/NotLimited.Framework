using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using NotLimited.Framework.Web.Helpers;
using NotLimited.Framework.Web.Views.Shared.Helpers;

namespace NotLimited.Framework.Web.Controls
{
    public static class FormExtensions
    {
        /// <summary>
        /// Gets an entry point to form extensions.
        /// </summary>
        public static FormHelper<T> Form<T>(this HtmlHelper<T> helper)
        {
            return new FormHelper<T>(helper);
        }

        /// <summary>
        /// Gets an entry point to form extensions.
        /// </summary>
        public static FormHelper Form(this HtmlHelper helper)
        {
            return new FormHelper(helper);
        }

        /// <summary>
        /// Creates a form with specified method and current action and controller.
        /// </summary>
        public static MvcForm BeginForm(this HtmlHelper htmlHelper, FormMethod method, object htmlAttributes = null)
        {
            return htmlHelper.BeginForm(null, null, method, htmlAttributes.ConcatHtmlAttributes(new {role = "form"}));
        }

        /// <summary>
        /// Creates a form suitable for an upload control.
        /// </summary>
        public static MvcForm BeginUploadForm<T>(this FormHelper<T> helper, string action = null, string controller = null)
        {
            return helper.HtmlHelper.BeginForm(action, controller, FormMethod.Post, new { enctype = "multipart/form-data", role = "form" });
        }
        
        /// <summary>
        /// Creates a form with POST method.
        /// </summary>
        public static MvcForm BeginPostForm<T>(this FormHelper<T> helper, string action = null, string controller = null, string id = null)
        {
            return helper.HtmlHelper.BeginForm(action, controller, FormMethod.Post, new {role = "form", id = id});
        }
    }
}