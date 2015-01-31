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
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using NotLimited.Framework.Web;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal class FormHelpers : System.Web.WebPages.HelperPage
    {

#line default
#line hidden
public static System.Web.WebPages.HelperResult TextBox(MvcHtmlString label, MvcHtmlString textBox, MvcHtmlString validation)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {
 

WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"form-group\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "        ");

WriteTo(__razor_helper_writer, label);

WriteLiteralTo(__razor_helper_writer, "\r\n");

WriteLiteralTo(__razor_helper_writer, "        ");

WriteTo(__razor_helper_writer, textBox);

WriteLiteralTo(__razor_helper_writer, "\r\n");

WriteLiteralTo(__razor_helper_writer, "        ");

WriteTo(__razor_helper_writer, validation);

WriteLiteralTo(__razor_helper_writer, "\r\n    </div>\r\n");


});

#line default
#line hidden
}
#line default
#line hidden

#line default
#line hidden
public static System.Web.WebPages.HelperResult Upload(MvcHtmlString label, string inputName, MvcHtmlString validation, string accept = "image/*")
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {
 

WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"form-group\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "        ");

WriteTo(__razor_helper_writer, label);

WriteLiteralTo(__razor_helper_writer, "\r\n        <input");

WriteLiteralTo(__razor_helper_writer, " type=\"file\"");

WriteLiteralTo(__razor_helper_writer, " class=\"form-control\"");

WriteAttributeTo(__razor_helper_writer, "name", Tuple.Create(" name=\"", 513), Tuple.Create("\"", 530)
, Tuple.Create(Tuple.Create("", 520), Tuple.Create<System.Object, System.Int32>(inputName
, 520), false)
);

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 531), Tuple.Create("\"", 546)
, Tuple.Create(Tuple.Create("", 536), Tuple.Create<System.Object, System.Int32>(inputName
, 536), false)
);

WriteAttributeTo(__razor_helper_writer, "accept", Tuple.Create(" accept=\"", 547), Tuple.Create("\"", 563)
           , Tuple.Create(Tuple.Create("", 556), Tuple.Create<System.Object, System.Int32>(accept
, 556), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "        ");

WriteTo(__razor_helper_writer, validation);

WriteLiteralTo(__razor_helper_writer, "\r\n    </div>\r\n");


});

#line default
#line hidden
}
#line default
#line hidden

#line default
#line hidden
public static System.Web.WebPages.HelperResult InputWithButton(MvcHtmlString input, MvcHtmlString button)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {
 

WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"input-group\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "        ");

WriteTo(__razor_helper_writer, input);

WriteLiteralTo(__razor_helper_writer, "\r\n        <span");

WriteLiteralTo(__razor_helper_writer, " class=\"input-group-btn\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "            ");

WriteTo(__razor_helper_writer, button);

WriteLiteralTo(__razor_helper_writer, "\r\n        </span>\r\n    </div>\r\n");


});

#line default
#line hidden
}
#line default
#line hidden

    }
}
#pragma warning restore 1591
