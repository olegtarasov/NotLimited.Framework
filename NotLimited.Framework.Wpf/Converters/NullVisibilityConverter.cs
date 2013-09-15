using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NotLimited.Framework.Wpf.Converters
{
	[ValueConversion(typeof(object), typeof(Visibility))]
	public class NullVisibilityConverter : IValueConverter
	{
		public object Convert(object obj, Type targetType, object parameter, CultureInfo culture)
		{
			return obj == null ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}