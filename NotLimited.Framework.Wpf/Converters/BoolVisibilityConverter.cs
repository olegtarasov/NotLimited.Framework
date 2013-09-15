using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NotLimited.Framework.Wpf.Converters
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class BoolVisibilityConverter : IValueConverter
	{
		#region Implementation of IValueConverter

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool invert = parameter != null && System.Convert.ToBoolean(parameter);

			if ((bool)value)
				return invert ? Visibility.Collapsed : Visibility.Visible;
			
			return invert ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		#endregion
	}
}