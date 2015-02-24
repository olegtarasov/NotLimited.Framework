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
    using NotLimited.Framework.Web.Controls;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal class DialogHelpers : System.Web.WebPages.HelperPage
    {

#line default
#line hidden
public static System.Web.WebPages.HelperResult ModalDialog(string id, string title, Func<object, HelperResult> body, Func<IDisposable> form, string okHandler, string okText, string cancelText)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {
 

WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"modal fade\"");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 373), Tuple.Create("\"", 381)
, Tuple.Create(Tuple.Create("", 378), Tuple.Create<System.Object, System.Int32>(id
, 378), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n        <div");

WriteLiteralTo(__razor_helper_writer, " class=\"modal-dialog\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

            
             using (form != null ? form() : new DummyForm())
            {

WriteLiteralTo(__razor_helper_writer, "                <div");

WriteLiteralTo(__razor_helper_writer, " class=\"modal-content\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"modal-header\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                        <a");

WriteLiteralTo(__razor_helper_writer, " href=\"#\"");

WriteLiteralTo(__razor_helper_writer, " class=\"close\"");

WriteLiteralTo(__razor_helper_writer, " data-dismiss=\"modal\"");

WriteLiteralTo(__razor_helper_writer, " aria-hidden=\"true\"");

WriteLiteralTo(__razor_helper_writer, ">×</a>\r\n                        <h4");

WriteLiteralTo(__razor_helper_writer, " class=\"modal-title\"");

WriteLiteralTo(__razor_helper_writer, ">");

                  WriteTo(__razor_helper_writer, title);

WriteLiteralTo(__razor_helper_writer, "</h4>\r\n                    </div>\r\n\r\n                    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"modal-body\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "                        ");

WriteTo(__razor_helper_writer, body(null));

WriteLiteralTo(__razor_helper_writer, "\r\n                    </div>\r\n\r\n                    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"modal-footer\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

                        
                         if (!string.IsNullOrEmpty(cancelText))
                        {

WriteLiteralTo(__razor_helper_writer, "                            <button");

WriteLiteralTo(__razor_helper_writer, " type=\"button\"");

WriteLiteralTo(__razor_helper_writer, " class=\"btn btn-default\"");

WriteLiteralTo(__razor_helper_writer, " data-dismiss=\"modal\"");

WriteLiteralTo(__razor_helper_writer, ">");

                                                                 WriteTo(__razor_helper_writer, cancelText);

WriteLiteralTo(__razor_helper_writer, "</button>\r\n");

                        }

WriteLiteralTo(__razor_helper_writer, "                        ");

                         if (!string.IsNullOrEmpty(okHandler))
                        {

WriteLiteralTo(__razor_helper_writer, "                            <button");

WriteLiteralTo(__razor_helper_writer, " data-dismiss=\"modal\"");

WriteLiteralTo(__razor_helper_writer, " type=\"button\"");

WriteLiteralTo(__razor_helper_writer, " class=\"btn btn-primary\"");

WriteAttributeTo(__razor_helper_writer, "onclick", Tuple.Create(" onclick=\"", 1361), Tuple.Create("\"", 1381)
                        , Tuple.Create(Tuple.Create("", 1371), Tuple.Create<System.Object, System.Int32>(okHandler
, 1371), false)
);

WriteLiteralTo(__razor_helper_writer, ">");

                                                                                      WriteTo(__razor_helper_writer, okText);

WriteLiteralTo(__razor_helper_writer, "</button>\r\n");

                        }
                        else
                        {
                            if (form == null)
                            {

WriteLiteralTo(__razor_helper_writer, "                                <button");

WriteLiteralTo(__razor_helper_writer, " type=\"button\"");

WriteLiteralTo(__razor_helper_writer, " class=\"btn btn-primary\"");

WriteLiteralTo(__razor_helper_writer, " data-dismiss=\"modal\"");

WriteLiteralTo(__razor_helper_writer, ">");

                                                                     WriteTo(__razor_helper_writer, okText);

WriteLiteralTo(__razor_helper_writer, "</button>\r\n");

                            }
                            else
                            {

WriteLiteralTo(__razor_helper_writer, "                                <button");

WriteLiteralTo(__razor_helper_writer, " type=\"submit\"");

WriteLiteralTo(__razor_helper_writer, " class=\"btn btn-primary\"");

WriteLiteralTo(__razor_helper_writer, ">");

                                                WriteTo(__razor_helper_writer, okText);

WriteLiteralTo(__razor_helper_writer, "</button>\r\n");

                            }
                        }

WriteLiteralTo(__razor_helper_writer, "                    </div>\r\n                </div>\r\n");

            }

WriteLiteralTo(__razor_helper_writer, "        </div>\r\n    </div>\r\n");


});

#line default
#line hidden
}
#line default
#line hidden

    }
}
#pragma warning restore 1591
