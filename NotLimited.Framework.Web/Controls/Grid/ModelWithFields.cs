using System.Collections.Generic;

namespace NotLimited.Framework.Web.Controls.Grid
{
	public class ModelWithFields<T>
	{
		public ModelWithFields(T model, HashSet<string> fields)
		{
			Model = model;
			Fields = fields;
		}

		public T Model { get; set; }
		public HashSet<string> Fields { get; set; }
	}
}