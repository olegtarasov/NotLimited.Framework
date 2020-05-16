using System.Windows;
using System.Windows.Data;

namespace NotLimited.Framework.Wpf
{
	public class Inherit : Binding
	{
		public Inherit(string elementName)
		{
			ElementName = elementName;
			Path = new PropertyPath("DataContext");
		}

		public Inherit(string elementName, string path)
		{
			ElementName = elementName;
			Path = new PropertyPath("DataContext." + path);
		}
	}
}