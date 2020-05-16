using System.Web.Mvc;

namespace NotLimited.Framework.Web.Mvc
{
	public class CSVAttribute : CustomModelBinderAttribute
	{
		public override IModelBinder GetBinder()
		{
			return new CommaSeparatedModelBinder();
		}
	}
}