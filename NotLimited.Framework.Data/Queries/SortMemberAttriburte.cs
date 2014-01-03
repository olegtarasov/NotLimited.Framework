using System;

namespace NotLimited.Framework.Data.Queries
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SortMemberAttribute : Attribute
	{
		public SortMemberAttribute(string name)
		{
			Name = name;
		}

		public string Name { get; set; }
	}
}