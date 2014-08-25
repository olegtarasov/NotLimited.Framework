using System.Windows;

namespace NotLimited.Framework.Wpf
{
	public static class WpfConversionExtensions
	{
		 public static System.Drawing.Point ToDrawingPoint(this Point point)
		 {
			 return new System.Drawing.Point((int)point.X, (int)point.Y);
		 }
	}
}