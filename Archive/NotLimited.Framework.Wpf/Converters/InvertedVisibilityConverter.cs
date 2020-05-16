using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NotLimited.Framework.Wpf.Converters
{
	[ValueConversion(typeof(Visibility), typeof(Visibility))]
	public class InvertedVisibilityConverter : IValueConverter
	{
		public object Convert(object obj, Type targetType, object parameter, CultureInfo culture)
		{
			if (obj == null)
				return null;

			return ((Visibility)obj) == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}