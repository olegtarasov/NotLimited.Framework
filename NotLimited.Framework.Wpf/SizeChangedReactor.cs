using System.Collections.Generic;
using System.Windows;

namespace NotLimited.Framework.Wpf
{
	public class SizeChangedReactor
	{
		private readonly Queue<SizeChangedEventHandler> _handlers = new Queue<SizeChangedEventHandler>();

		public SizeChangedReactor Then(SizeChangedEventHandler handler)
		{
			_handlers.Enqueue(handler);
			return this;
		}

		public static SizeChangedReactor AfterSizeChanged(FrameworkElement element, SizeChangedEventHandler handler)
		{
			return new SizeChangedReactor(element, handler);
		}

		private SizeChangedReactor(FrameworkElement element, SizeChangedEventHandler handler)
		{
			_handlers.Enqueue(handler);
			element.SizeChanged += OnSizeChanged;
		}

		private void OnSizeChanged(object sender, SizeChangedEventArgs args)
		{
			var handler = _handlers.Dequeue();
			if (_handlers.Count == 0)
				((FrameworkElement)sender).SizeChanged -= OnSizeChanged;

			handler(sender, args);
		}
	}
}