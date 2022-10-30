using Xunit;

namespace NotLimited.Framework.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var b = new UriBuilder("C:\\foo.bar");
    }
}