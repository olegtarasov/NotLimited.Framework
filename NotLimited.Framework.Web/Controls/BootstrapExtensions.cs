using System.Web.Mvc;

namespace NotLimited.Framework.Web.Controls
{
	public class OdinHelper
	{
		public OdinHelper(HtmlHelper helper)
		{
			HtmlHelper = helper;
		}

		public HtmlHelper HtmlHelper { get; private set; }
	}

	public class OdinHelper<T> : OdinHelper
	{
		public OdinHelper(HtmlHelper<T> helper) : base(helper)
		{
		}

		public new HtmlHelper<T> HtmlHelper { get { return (HtmlHelper<T>)base.HtmlHelper; } }
	}

	public static class BootstrapExtensions
	{
		 public static OdinHelper<T> Odin<T>(this HtmlHelper<T> helper)
		 {
			 return new OdinHelper<T>(helper);
		 }
	}
}