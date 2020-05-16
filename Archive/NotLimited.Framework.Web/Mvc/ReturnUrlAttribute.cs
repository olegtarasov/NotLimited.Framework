using System;
using System.Web.Mvc;

namespace NotLimited.Framework.Web.Mvc
{
    /// <summary>
    /// Sets ReturnUrl in TempData to specified controller and action 
    /// or current controller and action if not specified.
    /// </summary>
    public class ReturnUrlAttribute : ActionFilterAttribute
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string controller = string.IsNullOrEmpty(Controller)
                                    ? Convert.ToString(filterContext.RouteData.Values["controller"])
                                    : Controller;

            string action = string.IsNullOrEmpty(Action)
                                ? Convert.ToString(filterContext.RouteData.Values["action"])
                                : Action;

            filterContext.Controller.TempData[ViewDataKeys.ReturnUrl] = new UrlHelper(filterContext.RequestContext).Action(action, controller);
        }
    }
}