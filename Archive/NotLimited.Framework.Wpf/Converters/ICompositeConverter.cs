﻿using System.Windows.Data;

namespace NotLimited.Framework.Wpf.Converters
{
	public interface ICompositeConverter : IValueConverter
	{
		IValueConverter PostConverter { get; set; }
		object PostConverterParameter { get; set; }
	}
}
