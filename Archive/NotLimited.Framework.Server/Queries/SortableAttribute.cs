using System;

namespace NotLimited.Framework.Server.Queries
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SortableAttribute : Attribute
	{
		public string MemberName { get; set; }
	}
}