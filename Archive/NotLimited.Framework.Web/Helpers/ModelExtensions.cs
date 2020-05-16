using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace NotLimited.Framework.Web.Helpers
{
    public static class ModelExtensions
    {
        public static MvcHtmlString GetModelValidationMessage(this ModelStateDictionary modelState)
        {
            var stringBuilder = new StringBuilder();
            var ul = new TagBuilder("ul");
            foreach (var error in modelState.Values.SelectMany(x => x.Errors))
            {
                string messageOrDefault = error.ErrorMessage;
                if (!string.IsNullOrEmpty(messageOrDefault))
                {
                    var li = new TagBuilder("li");
                    li.SetInnerText(messageOrDefault);
                    stringBuilder.AppendLine(li.ToString(TagRenderMode.Normal));
                }
            }
            if (stringBuilder.Length == 0)
                stringBuilder.AppendLine("<li style=\"display:none\"></li>");
            ul.InnerHtml = stringBuilder.ToString();
            
            var div = new TagBuilder("div");
            div.AddCssClass(modelState.IsValid ? HtmlHelper.ValidationSummaryValidCssClassName : HtmlHelper.ValidationSummaryCssClassName);
            div.InnerHtml = ul.ToString(TagRenderMode.Normal);

            return new MvcHtmlString(div.ToString());
        }
    }
}