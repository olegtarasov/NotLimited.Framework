using System;

namespace NotLimited.Framework.Data.Queries
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SortableAttribute : Attribute
	{
		public string MemberName { get; set; }
	}
}