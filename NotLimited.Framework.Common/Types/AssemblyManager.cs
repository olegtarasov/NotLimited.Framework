using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NotLimited.Framework.Common.Types;

/// <summary>
/// Loads assemblies with support for non-standard locations and helps to resolve types from these assemblies.
/// </summary>
/// <remarks>
/// Useful for plugin management.
/// </remarks>
public class AssemblyManager
{
    private readonly HashSet<string> _resolvePaths = new();
    private readonly Dictionary<string, Assembly> _assemblies = new();

    /// <summary>
    /// Ctor.
    /// </summary>
    public AssemblyManager()
    {
        _resolvePaths.Add(Environment.CurrentDirectory.ToLowerInvariant());

        if (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) is { } location)
            _resolvePaths.Add(location.ToLowerInvariant());

        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    /// <summary>
    /// A list of loaded assemblies.
    /// </summary>
    public IEnumerable<Assembly> Assemblies => _assemblies.Values;

    /// <summary>
    /// Loads assemblies from directory where framework assembly is located.
    /// </summary>
    public void LoadLocalAssemblies()
    {
        string? location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (string.IsNullOrEmpty(location))
            return;

        LoadAssemblies(Directory.GetFiles(location, "*.dll"));
    }

    /// <summary>
    /// Loads assemblies specified by their paths.
    /// </summary>
    public void LoadAssemblies(IEnumerable<string> paths)
    {
        foreach (var path in paths)
            LoadAssembly(path);
    }

    /// <summary>
    /// Loads an assembly from a specified path (absolute or relative). If path is absolute, it is added to
    /// possible resolve locations along with all its subtrees.
    /// </summary>
    public void LoadAssembly(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentNullException(nameof(path));

        if (Path.IsPathRooted(path))
        {
            string? resolvePath = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(resolvePath))
            {
                foreach (var p in CollectResolvePaths(resolvePath))
                    _resolvePaths.Add(p);
            }
        }

        _assemblies.Add(path.ToLowerInvariant(), ResolveAssembly(path));
    }

    /// <summary>
    /// Gets types from all loaded assemblies using a specified predicate.
    /// </summary>
    public IEnumerable<Type> GetTypes(Func<Type, bool> predicate)
    {
        return _assemblies.Values.SelectMany(assembly => assembly.GetTypes().Where(predicate));
    }

    /// <summary>
    /// Gets types from a specified assembly file using a predicate.
    /// </summary>
    public IEnumerable<Type> GetTypes(string assemblyFile, Func<Type, bool> predicate)
    {
        string lowerFile = assemblyFile.ToLowerInvariant();

        if (!_assemblies.ContainsKey(lowerFile))
            LoadAssembly(lowerFile);

        return _assemblies[lowerFile].GetTypes().Where(predicate);
    }

    /// <summary>
    /// Tries to find a type in all loaded assemblies.
    /// </summary>
    public Type? GetType(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
            throw new ArgumentNullException(nameof(typeName));

        foreach (var assembly in _assemblies.Values)
        {
            var type = assembly.GetType(typeName);
            if (type != null)
                return type;
        }

        return null;
    }

    /// <summary>
    /// Tries to find a type in a specific assembly.
    /// </summary>
    public Type? GetType(string assemblyPath, string type)
    {
        if (string.IsNullOrEmpty(assemblyPath))
            throw new ArgumentNullException(nameof(assemblyPath));
        if (string.IsNullOrEmpty(type))
            throw new ArgumentNullException(nameof(type));

        string lowerFile = assemblyPath.ToLowerInvariant();

        if (!_assemblies.ContainsKey(lowerFile))
            LoadAssembly(lowerFile);

        return _assemblies[lowerFile].GetType(type, false);
    }

    /// <summary>
    /// Tries to load an assembly if fileName is an absolute path, or tries to resolve it from
    /// possible resolve locations.
    /// </summary>
    private Assembly ResolveAssembly(string fileName)
    {
        if (Path.IsPathRooted(fileName))
            return LoadAssemblyFile(fileName)
                   ?? throw new InvalidOperationException("Can't load the assembly: " + fileName);

        foreach (var resolvePath in _resolvePaths)
        {
            string path = Path.Combine(resolvePath, fileName);
            var assembly = LoadAssemblyFile(path);
            if (assembly != null)
                return assembly;
        }

        throw new InvalidOperationException("Can't find the assembly: " + fileName);
    }

    private Assembly? LoadAssemblyFile(string path)
    {
        try
        {
            return Assembly.LoadFile(path);
        }
        catch
        {
            return null;
        }
    }

    private IEnumerable<string> CollectResolvePaths(string root)
    {
        var queue = new Queue<string>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            string dir = queue.Dequeue();

            foreach (var child in Directory.GetDirectories(dir))
                queue.Enqueue(child);

            yield return dir.ToLowerInvariant();
        }
    }

    private Assembly? CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
    {
        if (args.RequestingAssembly != null
            && Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) is { } requestingDir)
        {
            _resolvePaths.Add(requestingDir.ToLowerInvariant());
        }

        var name = new AssemblyName(args.Name);

        foreach (var resolvePath in _resolvePaths)
        {
            string path = Path.Combine(resolvePath, name.Name + ".dll");
            if (!File.Exists(path))
                continue;

            try
            {
                return Assembly.LoadFile(path);
            }
            catch
            {
                // We tried and failed, but will continue trying other locations.
            }
        }

        return null;
    }
}