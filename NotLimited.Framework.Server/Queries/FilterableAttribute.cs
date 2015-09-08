using System;

namespace NotLimited.Framework.Server.Queries
{
	[AttributeUsage(AttributeTargets.Property)]
	public class FilterableAttribute : Attribute
	{
		public string MemberName { get; set; }
	}
}