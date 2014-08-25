using System;

namespace NotLimited.Framework.Data.Queries
{
	[AttributeUsage(AttributeTargets.Property)]
	public class FilterableAttribute : Attribute
	{
		public string MemberName { get; set; }
	}
}