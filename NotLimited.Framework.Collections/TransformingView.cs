using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace NotLimited.Framework.Collections
{
	public sealed class TransformingView<T, E> : INotifyCollectionChanged, IDisposable, IEnumerable<E>
	{
		private readonly IEnumerable<T> collection;
		private readonly Func<T, IEnumerable<E>> transformer;
		private readonly INotifyCollectionChanged notify;

		public TransformingView(IEnumerable<T> collection, Func<T, IEnumerable<E>> transformer)
		{
			this.collection = collection;
			this.transformer = transformer;

			notify = collection as INotifyCollectionChanged;

			if (notify != null)
				notify.CollectionChanged += notify_CollectionChanged;
		}

		private void notify_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventArgs args = null;

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					args = new NotifyCollectionChangedEventArgs(e.Action, e.NewItems.Cast<T>().Select(transformer).ToList(), RecalculateIndex(e.NewStartingIndex));
					break;
				case NotifyCollectionChangedAction.Remove:
					args = new NotifyCollectionChangedEventArgs(e.Action, e.OldItems.Cast<T>().Select(transformer).ToList(), RecalculateIndex(e.OldStartingIndex));
					break;
				case NotifyCollectionChangedAction.Replace:
					args = new NotifyCollectionChangedEventArgs(e.Action, e.NewItems.Cast<T>().Select(transformer).ToList(), e.OldItems.Cast<T>().Select(transformer).ToList(), RecalculateIndex(e.OldStartingIndex));
					break;
				case NotifyCollectionChangedAction.Move:
					throw new InvalidOperationException();
				case NotifyCollectionChangedAction.Reset:
					args = e;
					break;
			}

			OnCollectionChanged(args);
		}

		private int RecalculateIndex(int idx)
		{
			return collection.Take(idx).Sum(x => transformer(x).Count());
		}

		private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if (CollectionChanged != null)
				CollectionChanged(this, e);
		}
		public event NotifyCollectionChangedEventHandler CollectionChanged;
		public void Dispose()
		{
			if (notify != null)
				notify.CollectionChanged -= notify_CollectionChanged;
		}

		public IEnumerator<E> GetEnumerator()
		{
			return new TransformingEnumerator<T, E>(collection.GetEnumerator(), transformer);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
