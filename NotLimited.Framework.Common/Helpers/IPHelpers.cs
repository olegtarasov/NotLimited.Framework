using System;
using System.Net;
using System.Text.RegularExpressions;

namespace NotLimited.Framework.Common.Helpers
{
	public static class IPHelpers
	{
		private static readonly Regex _ipRegex = new Regex(@"Your IP is\s+<b>(.*)</b>");

		public static IPAddress GetExternalIp()
		{
			var client = new WebClient();

			try
			{
				string result = client.DownloadString("http://ping.eu/");
				var match = _ipRegex.Match(result);

				if (!match.Success || match.Groups.Count < 2)
				{
					return null;
				}

				return IPAddress.Parse(match.Groups[1].Value);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}