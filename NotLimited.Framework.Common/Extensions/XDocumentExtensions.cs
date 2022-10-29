using System.Xml.Linq;
using System.Xml.Serialization;

namespace NotLimited.Framework.Common.Extensions;

/// <summary>
/// Helpers to work with <see cref="XDocument"/>.
/// </summary>
public static class XDocumentExtensions
{
    /// <summary>
    /// Deserializes an object from <see cref="XElement"/>.
    /// </summary>
    public static T? Deserialize<T>(this XElement node)
    {
        var reader = node.CreateReader();
        var serializer = new XmlSerializer(typeof(T));

        return (T?)serializer.Deserialize(reader);
    }

    /// <summary>
    /// Finds the first child with specified name.
    /// </summary>
    public static XElement? ChildElement(this XContainer node, string name)
    {
        return node.Elements().FirstOrDefault(x => x.Name.LocalName == name);
    }

    /// <summary>
    /// Finds all child elemnts with specified name.
    /// </summary>
    public static IEnumerable<XElement> ChildElements(this XContainer node, string name)
    {
        return node.Elements().Where(x => x.Name.LocalName == name);
    }

    /// <summary>
    /// Return a descendant element from hierarchy specified by node names.
    /// </summary>
    public static XElement? DescendantElement(this XContainer node, params string[] names)
    {
        var cur = node;

        for (int i = 0; i < names.Length; i++)
        {
            cur = node.ChildElement(names[i]);
            if (cur == null)
                return null;
        }

        return cur as XElement;
    }

    /// <summary>
    /// Returns a value of an attribute.
    /// </summary>
    public static string? AttributeValue(this XElement element, string name)
    {
        var attr = element.Attributes().FirstOrDefault(a => a.Name.LocalName == name);

        if (attr != null)
            return attr.Value;

        return null;
    }

    /// <summary>
    /// Adds a child element to a specified node.
    /// </summary>
    public static void AddElement(this XElement element, XElement child)
    {
        element.Add(child);
        child.Name = XName.Get(child.Name.LocalName, element.Name.NamespaceName);
    }

    /// <summary>
    /// Adds child elements to a specified node.
    /// </summary>
    public static void AddElements(this XElement element, IEnumerable<XElement> children)
    {
        foreach (var child in children)
        {
            element.Add(child);
            element.SetElementsNamespace();
        }
    }

    /// <summary>
    /// Updates element local name preserving the namespace.
    /// </summary>
    public static void SetLocalName(this XElement element, string name)
    {
        element.Name = XName.Get(name, element.Name.NamespaceName);
    }

    private static void SetElementsNamespace(this XElement parent)
    {
        foreach (var descendant in parent.Descendants())
            descendant.Name = XName.Get(descendant.Name.LocalName, parent.Name.NamespaceName);
    }
}