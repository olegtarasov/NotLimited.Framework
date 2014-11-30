﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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
    
    #line 2 "..\..\Views\Shared\Helpers\FilterViewHelper.cshtml"
    using System.Web.Mvc.Html;
    
    #line default
    #line hidden
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 3 "..\..\Views\Shared\Helpers\FilterViewHelper.cshtml"
    using NotLimited.Framework.Web.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public static class FilterViewHelper
    {

public static System.Web.WebPages.HelperResult FilterBox(System.Web.Mvc.HtmlHelper helper, string name, string title, IEnumerable<System.Web.Mvc.SelectListItem> items, string placeholder)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 6 "..\..\Views\Shared\Helpers\FilterViewHelper.cshtml"
 
    
#line default
#line hidden


#line 7 "..\..\Views\Shared\Helpers\FilterViewHelper.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, helper.DropDownList(name, items, new {@class = "select2", data_select_search = true, data_placeholder = placeholder}));

#line default
#line hidden


#line 7 "..\..\Views\Shared\Helpers\FilterViewHelper.cshtml"
                                                                                                                            

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult HiddenForQuery(System.Web.Mvc.HtmlHelper html, params string[] except)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 11 "..\..\Views\Shared\Helpers\FilterViewHelper.cshtml"
 
    foreach (var item in html.QueryStringToRouteDictionary().Where(x => !except.Contains(x.Key)))
    {
        
#line default
#line hidden


#line 14 "..\..\Views\Shared\Helpers\FilterViewHelper.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, html.Hidden(item.Key, item.Value));

#line default
#line hidden


#line 14 "..\..\Views\Shared\Helpers\FilterViewHelper.cshtml"
                                          
    }

#line default
#line hidden

});

}


    }
}
#pragma warning restore 1591