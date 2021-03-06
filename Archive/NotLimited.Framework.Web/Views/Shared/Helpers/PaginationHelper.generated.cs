﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotLimited.Framework.Web.Views.Shared.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using NotLimited.Framework.Server.Queries;
    using NotLimited.Framework.Web;
    using NotLimited.Framework.Web.Helpers;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal class PaginationHelper : System.Web.WebPages.HelperPage
    {

#line default
#line hidden
public static System.Web.WebPages.HelperResult RenderPage(HtmlHelper htmlHelper, int page, int currentPage, string action = null, string controller = null)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {
 

WriteLiteralTo(__razor_helper_writer, "    <li");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 357), Tuple.Create("\"", 405)
, Tuple.Create(Tuple.Create("", 365), Tuple.Create<System.Object, System.Int32>(page == currentPage ? "active" : null
, 365), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "        ");

WriteTo(__razor_helper_writer, htmlHelper.ActionLink((page).ToString(), action, controller, htmlHelper.AppendQueryString(new { page }), null));

WriteLiteralTo(__razor_helper_writer, "\r\n    </li>\r\n");


});

#line default
#line hidden
}
#line default
#line hidden

#line default
#line hidden
public static System.Web.WebPages.HelperResult DisabledPage()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {
 

WriteLiteralTo(__razor_helper_writer, "    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"disabled\"");

WriteLiteralTo(__razor_helper_writer, "><span>...</span></li>\r\n");


});

#line default
#line hidden
}
#line default
#line hidden

#line default
#line hidden
public static System.Web.WebPages.HelperResult Paginator(System.Web.Mvc.HtmlHelper htmlHelper, Pagination pagination, string wrapperClass = "text-center", string listClass = "pagination", string action = null, string controller = null)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {
 
    int pageCount = pagination.PageCount;
    if (pageCount > 1)
    {

WriteLiteralTo(__razor_helper_writer, "        <div");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 913), Tuple.Create("\"", 934)
, Tuple.Create(Tuple.Create("", 921), Tuple.Create<System.Object, System.Int32>(wrapperClass
, 921), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n            <ul");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 953), Tuple.Create("\"", 971)
, Tuple.Create(Tuple.Create("", 961), Tuple.Create<System.Object, System.Int32>(listClass
, 961), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n");

                
                 if (pageCount > 6)
                {
                    if (pagination.Page < 5)
                    {
                        foreach (var page in Enumerable.Range(1, Math.Max(3, pagination.Page + 1)))
                        {
                            
WriteTo(__razor_helper_writer, RenderPage(htmlHelper, page, pagination.Page, action, controller));

                                                                                              
                        }
                    }
                    else
                    {
                        
WriteTo(__razor_helper_writer, RenderPage(htmlHelper, 1, pagination.Page, action, controller));

                                                                                       
                    }

                    
WriteTo(__razor_helper_writer, DisabledPage());

                                   

                    if (pagination.Page > pageCount - 4)
                    {
                        int start = Math.Min(pagination.Page - 1, pageCount - 2);

                        foreach (var page in Enumerable.Range(start, pageCount - start + 1))
                        {
                            
WriteTo(__razor_helper_writer, RenderPage(htmlHelper, page, pagination.Page, action, controller));

                                                                                              
                        }
                    }
                    else
                    {
                        if (pagination.Page >= 5)
                        {
                            foreach (var page in Enumerable.Range(pagination.Page - 1, 3))
                            {
                                
  WriteTo(__razor_helper_writer, RenderPage(htmlHelper, page, pagination.Page, action, controller));

                                                                                                  
                            }

                            
WriteTo(__razor_helper_writer, DisabledPage());

                                           
                        }

                        
WriteTo(__razor_helper_writer, RenderPage(htmlHelper, pageCount, pagination.Page, action, controller));

                                                                                               
                    }
                }
                else
                {
                    foreach (var page in Enumerable.Range(1, pageCount))
                    {
                        
WriteTo(__razor_helper_writer, RenderPage(htmlHelper, page, pagination.Page, action, controller));

                                                                                          
                    }
                }

WriteLiteralTo(__razor_helper_writer, "            </ul>\r\n        </div>\r\n");

    }

});

#line default
#line hidden
}
#line default
#line hidden

    }
}
#pragma warning restore 1591
