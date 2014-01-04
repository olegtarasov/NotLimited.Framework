﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using NotLimited.Framework.Common.Helpers;

namespace NotLimited.Framework.Data.Queries
{
	public class PropertyMetadata
	{
		public PropertyInfo PropertyInfo { get; set; }
		public string Description { get; set; }
		public string SortMember { get; set; }
		public bool Sortable { get; set; }
		public string FilterMember { get; set; }
		public bool Filterable { get; set; }
	}

	public static class PropertyMetadataCache<T>
	{
		private static Dictionary<string, PropertyMetadata> _propertyCache;

		public static PropertyMetadata GetPropertyMetadata(string propertyName)
		{
			ReadPropertyCache();

			if (!_propertyCache.ContainsKey(propertyName))
				throw new InvalidOperationException("Property " + propertyName + " not found!");

			return _propertyCache[propertyName];
		}

		public static PropertyMetadata GetPropertyMetadata<TKey>(Expression<Func<T, TKey>> expression)
		{
			ReadPropertyCache();

			string propName = expression.GetMemberName();
			if (!_propertyCache.ContainsKey(propName))
				throw new InvalidOperationException("Property " + propName + " not found!");

			return _propertyCache[propName];
		}

		private static void ReadPropertyCache()
		{
			if (_propertyCache != null)
				return;

			_propertyCache = new Dictionary<string, PropertyMetadata>();
			foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				var metadata = new PropertyMetadata {PropertyInfo = propertyInfo};
				var descAttr = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
				var sortableAttr = propertyInfo.GetCustomAttribute<SortableAttribute>();
				var filterableAttr = propertyInfo.GetCustomAttribute<FilterableAttribute>();

				metadata.Description = (descAttr != null && !string.IsNullOrEmpty(descAttr.Description)) ? descAttr.Description : propertyInfo.Name;
				metadata.SortMember = sortableAttr != null ? sortableAttr.MemberName : null;
				metadata.Sortable = sortableAttr != null;
				metadata.FilterMember = filterableAttr != null ? filterableAttr.MemberName : null;
				metadata.Filterable = filterableAttr != null;

				_propertyCache[propertyInfo.Name] = metadata;
			}
		}
	}
}