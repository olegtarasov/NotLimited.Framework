using System;
using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class Token : IToken
	{
		public string Id { get; set; }
		public string Value { get; set; }
		public DateTime ValidUntilUtc { get; set; }
	}
}