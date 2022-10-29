using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace NotLimited.Framework.Cli.Spectre;

/// <summary>
/// A type resolver to use with Spectre.Console projects.
/// </summary>
public class TypeResolver : ITypeResolver, IDisposable
{
    private readonly IHost _host;

    /// <summary>
    /// Ctor.
    /// </summary>
    public TypeResolver(IHost provider)
    {
        _host = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    /// <inheritdoc />
    public object? Resolve(Type? type)
    {
        return type != null ? _host.Services.GetService(type) : null;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _host.Dispose();
    }
}