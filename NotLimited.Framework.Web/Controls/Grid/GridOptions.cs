using System;
using System.Collections.Generic;
using System.Web.WebPages;
using NotLimited.Framework.Server.Queries;

namespace NotLimited.Framework.Web.Controls.Grid
{
	public class GridOptions
	{
		public List<HelperResult> Headers { get; set; }
		public List<List<HelperResult>> Rows { get; set; }
		public Func<IDisposable> Form { get; set; }
		public Func<object, HelperResult> FormControls { get; set; }
		public Pagination Pagination { get; set; }
		public object TableHtmlAttributes { get; set; }
		public string Action { get; set; }
		public string Controller { get; set; }
    }
}