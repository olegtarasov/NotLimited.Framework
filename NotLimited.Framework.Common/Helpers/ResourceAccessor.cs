using System.Reflection;
using System.Text;

namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// A helper class to load resources from an assembly.
/// </summary>
public class ResourceAccessor
{
    private readonly Assembly _assembly;
    private readonly string _assemblyName;

    /// <summary>
    /// Creates a resource accessor for the specified assembly.
    /// </summary>
    public ResourceAccessor(Assembly assembly)
    {
        _assembly = assembly;
        string? resourceName = assembly.GetManifestResourceNames().FirstOrDefault();
        if (resourceName == null)
            throw new InvalidOperationException("Failed to get assembly name!");

        int pos = resourceName.IndexOf('.');

        _assemblyName = pos > -1 ? resourceName.Substring(0, pos) : resourceName;
    }

    /// <summary>
    /// Gets a resource with specified name as an array of bytes.
    /// </summary>
    /// <param name="name">Resource name with folders separated by dots.</param>
    /// <exception cref="InvalidOperationException">
    /// When resource is not found.
    /// </exception>
    public byte[] Binary(string name)
    {
        using (var stream = new MemoryStream())
        {
            var resource = _assembly.GetManifestResourceStream(GetName(name));
            if (resource == null)
                throw new InvalidOperationException($"Resource not available: {name}");

            resource.CopyTo(stream);

            return stream.ToArray();
        }
    }

    /// <summary>
    /// Gets a resource with specified name as a string.
    /// </summary>
    /// <param name="name">Resource name with folders separated by dots.</param>
    /// <exception cref="InvalidOperationException">
    /// When resource is not found.
    /// </exception>
    public string String(string name)
    {
        using (var stream = new MemoryStream())
        {
            var resource = _assembly.GetManifestResourceStream(GetName(name));
            if (resource == null)
                throw new InvalidOperationException($"Resource not available: {name}");

            resource.CopyTo(stream);

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }

    /// <summary>
    /// Gets a resource with specified name as a stream.
    /// </summary>
    /// <param name="name">Resource name with folders separated by dots.</param>
    /// <exception cref="InvalidOperationException">
    /// When resource is not found.
    /// </exception>
    public Stream Stream(string name)
    {
        var resource = _assembly.GetManifestResourceStream(GetName(name));
        if (resource == null)
            throw new InvalidOperationException($"Resource not available: {name}");

        return resource;
    }

    private string GetName(string name)
    {
        return name.StartsWith(_assemblyName) ? name : $"{_assemblyName}.{name}";
    }
}