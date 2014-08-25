using System;
using System.Web;

namespace NotLimited.Framework.Web.Helpers
{
	public static class RequestExtensions
	{
		 public static string GetBaseUrl(this HttpRequestBase request)
		 {
			 if (request == null) throw new ArgumentNullException("request");
			 if (request.Url == null) throw new ArgumentException("request.Url");

			 string scheme = request.Url.GetLeftPart(UriPartial.Scheme);
			 if (string.IsNullOrEmpty(scheme))
				 throw new InvalidOperationException();

			 string url = scheme + request.Url.Host;
			 if (request.Url.Port != 80)
				 url += ":" + request.Url.Port;

			 return url;
		 }
	}
}