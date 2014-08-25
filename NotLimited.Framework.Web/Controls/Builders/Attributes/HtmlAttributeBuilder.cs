using System;
using System.Collections.Generic;
using System.Linq;

namespace NotLimited.Framework.Web.Controls.Builders.Attributes
{
	public class HtmlAttributeBuilder
	{
		private const string ClassAttribute = "class";

		private readonly Dictionary<string, object> _attributes = new Dictionary<string, object>();

		public HtmlAttributeBuilder AddCssClass(string cssClass)
		{
			string curClass = _attributes.ContainsKey(ClassAttribute)
				                  ? (string)_attributes[ClassAttribute]
				                  : string.Empty;

			var classes = curClass.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			if (classes.Length != 0 && classes.Contains(cssClass)) 
				return this;

			if (curClass.Length > 0)
				curClass += " " + cssClass;
			else
				curClass += cssClass;

			_attributes[ClassAttribute] = curClass;

			return this;
		}

		public HtmlAttributeBuilder SetAttrubte(string attribute, string value)
		{
			_attributes[attribute] = value;

			return this;
		}

		public Dictionary<string, object> ToAttributeDictionary()
		{
			return _attributes;
		}
	}
}