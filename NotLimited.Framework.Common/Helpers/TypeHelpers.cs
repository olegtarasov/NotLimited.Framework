using System.Reflection;

namespace NotLimited.Framework.Common.Helpers;

/// <summary>
/// Helpers for working with types.
/// </summary>
public static class TypeHelpers
{
    /// <summary>
    /// Returns all public non-abstract types that are descendant from a specified type.
    /// </summary>
    /// <param name="allTypes">Types to filter.</param>
    /// <param name="ancestor">Ancestor type.</param>
    public static Type[] PublicConcreteDescendantsOf(this Type[] allTypes, Type ancestor)
    {
        return allTypes.Where(x => x.IsClass
                                   && x.IsPublic
                                   && !x.IsAbstract
                                   && (ancestor.IsGenericType
                                           ? x.IsAssignableToGenericType(ancestor)
                                           : x.IsAssignableTo(ancestor)))
                       .ToArray();
    }

    /// <summary>
    /// Returns all public non-abstract types that are marked with a specified attribute.
    /// </summary>
    /// <param name="allTypes">Types to filter.</param>
    /// <typeparam name="T">Attribute to check for.</typeparam>
    public static (Type type, T attribute)[] PublicConcreteMarkedWith<T>(this Type[] allTypes)
        where T : Attribute
    {
        return PublicConcreteDescendantsMarkedWith<T>(allTypes, null);
    }

    /// <summary>
    /// Returns all public non-abstract types that are marked with a specified attribute and optionally filters types
    /// by ancestor type.
    /// </summary>
    /// <param name="allTypes">Types to filter.</param>
    /// <param name="ancestor">Ancestor type.</param>
    /// <typeparam name="T">Attribute to check for.</typeparam>
    public static (Type type, T attribute)[] PublicConcreteDescendantsMarkedWith<T>(
        this Type[] allTypes,
        Type? ancestor)
        where T : Attribute
    {
        var candidates = from type in allTypes
                         where type != null
                         let attribute = type.GetCustomAttribute<T>()
                         where !type.IsAbstract && type.IsPublic && attribute != null
                         select (type, attribute);

        if (ancestor != null)
            candidates = candidates.Where(x => ancestor.IsGenericType
                                                   ? x.type.IsAssignableToGenericType(ancestor)
                                                   : x.type.IsAssignableTo(ancestor));

        return candidates.ToArray();
    }

    /// <summary>
    /// Checks if given type is assignable to specified generic type.
    /// </summary>
    /// <param name="givenType">Type to check.</param>
    /// <param name="genericType">Generic type.</param>
    /// <remarks>
    /// Method can handle generics without specified concrete type.
    /// </remarks>
    public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
    {
        var interfaceTypes = givenType.GetInterfaces();

        foreach (var it in interfaceTypes)
        {
            if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            return true;

        var baseType = givenType.BaseType;
        if (baseType == null)
            return false;

        return IsAssignableToGenericType(baseType, genericType);
    }
}