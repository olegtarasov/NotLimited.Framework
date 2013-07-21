using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NotLimited.Framework.Web.Controls.Attributes;

namespace NotLimited.Framework.Web.Plumbing
{
	public class ExtendedModelMetadataProvider : CachedDataAnnotationsModelMetadataProvider
	{
		protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
		{
			var metadata = base.CreateMetadataPrototype(attributes, containerType, modelType, propertyName);

			foreach (var pairs in attributes.OfType<MetadataAttribute>().Select(x => x.ToKeyValuePairs()))
			{
				foreach (var pair in pairs)
					metadata.AdditionalValues.Add(pair.Key, pair.Value);
			}

			return metadata;
		}

		protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
		{
			var metadata = base.CreateMetadataFromPrototype(prototype, modelAccessor);

			foreach (var pair in prototype.AdditionalValues)
			{
				if (!metadata.AdditionalValues.ContainsKey(pair.Key))
					metadata.AdditionalValues.Add(pair.Key, pair.Value);
			}

			return metadata;
		}
	}
}