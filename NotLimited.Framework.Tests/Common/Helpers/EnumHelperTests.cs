using NotLimited.Framework.Common.Helpers;
using Xunit;
using XunitShould;

namespace NotLimited.Framework.Tests.Common.Helpers
{
	public enum TestDefault
	{
		None = -1,
		One = 1,
		Two = 3,
		Three = 2
	}

	public class EnumHelperTests
	{
		[Fact]
		public void CanConvertEnumValueToArray()
		{
			var result = TestDefault.Three.ToDoubleArray();
			result.ShouldEnumerateEqual(0, 0, 1, 0);
		}
	}
}