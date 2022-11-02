using FluentAssertions;
using NotLimited.Framework.Common.Streams;
using Xunit;

namespace NotLimited.Framework.Tests.Common.Streams;

public class EolStreamsTests
{
    [Theory]
    [MemberData(nameof(GetCrLfData))]
    public void TestCrLfStreamRead(byte[] input, byte[] expected)
    {
        using var iStream = new MemoryStream(input);
        using var transformer = new ForceCrLfStream(iStream);
        using var oStream = new MemoryStream();
        
        transformer.CopyTo(oStream);
        oStream.ToArray().Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(GetCrLfData))]
    public void TestCrLfStreamWrite(byte[] input, byte[] expected)
    {
        using var iStream = new MemoryStream(input);
        using var oStream = new MemoryStream();
        using var transformer = new ForceCrLfStream(oStream);

        iStream.CopyTo(transformer);
        oStream.ToArray().Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(GetLfData))]
    public void TestLfStreamRead(byte[] input, byte[] expected)
    {
        using var iStream = new MemoryStream(input);
        using var transformer = new ForceLfStream(iStream);
        using var oStream = new MemoryStream();

        transformer.CopyTo(oStream);
        oStream.ToArray().Should().Equal(expected);
    }

    [Theory]
    [MemberData(nameof(GetLfData))]
    public void TestLfStreamWrite(byte[] input, byte[] expected)
    {
        using var iStream = new MemoryStream(input);
        using var oStream = new MemoryStream();
        using var transformer = new ForceLfStream(oStream);

        iStream.CopyTo(transformer);
        oStream.ToArray().Should().Equal(expected);
    }
    
    public static IEnumerable<object[]> GetCrLfData()
        => new[]
           {
               new object[] { Bytes("\n"), Bytes("\r\n") },
               new object[] { Bytes("\r\n"), Bytes("\r\n") },
               new object[] { Bytes("\n\n\n"), Bytes("\r\n\r\n\r\n") },
               new object[] { Bytes("\na\na\n"), Bytes("\r\na\r\na\r\n") },
               new object[] { Bytes("a\n\n\na"), Bytes("a\r\n\r\n\r\na") },
               new object[] { Bytes("aaa\n"), Bytes("aaa\r\n") },
               new object[] { Bytes("\naaa"), Bytes("\r\naaa") },
               new object[] { Bytes(""), Bytes("") },
           };

    public static IEnumerable<object[]> GetLfData()
        => new[]
           {
               new object[] { Bytes("\r\n"), Bytes("\n") },
               new object[] { Bytes("\r"), Bytes("\r") },
               new object[] { Bytes("\r\n\r\n\r\n"), Bytes("\n\n\n") },
               new object[] { Bytes("\r\r\n\r\n"), Bytes("\r\n\n") },
               new object[] { Bytes("\r\n\r\n\r"), Bytes("\n\n\r") },
               new object[] { Bytes("\r\na\r\na\n"), Bytes("\na\na\n") },
               new object[] { Bytes("a\r\n\r\na"), Bytes("a\n\na") },
               new object[] { Bytes("aaa\r\n"), Bytes("aaa\n") },
               new object[] { Bytes("\r\naaa"), Bytes("\naaa") },
               new object[] { Bytes(""), Bytes("") },
           };

    private static byte[] Bytes(string chars) => chars.Select(x => (byte)x).ToArray();
}