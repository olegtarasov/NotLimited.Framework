using System.Web.Mvc;
using Autofac.Features.Metadata;

namespace NotLimited.Framework.Web.Controls.Builders
{
	public static class MetadataExtensions
	{
		public static bool HasAdditionalValue(this ModelMetadata metadata, string key)
		{
			return metadata.AdditionalValues.ContainsKey(key);
		}

		public static T GetAdditionalValue<T>(this ModelMetadata metadata, string key)
		{
			return (T)metadata.AdditionalValues[key];
		}
	}
}