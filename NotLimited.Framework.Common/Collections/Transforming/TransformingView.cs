using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace NotLimited.Framework.Common.Collections.Transforming;

public sealed class TransformingView<TSource, TTarget> : INotifyCollectionChanged, IDisposable, IEnumerable<TTarget>
{
	private readonly IEnumerable<TSource> _collection;
	private readonly Func<TSource, IEnumerable<TTarget>> _transformer;
	private readonly INotifyCollectionChanged? _notify;

	public TransformingView(IEnumerable<TSource> collection, Func<TSource, IEnumerable<TTarget>> transformer)
	{
		_collection = collection;
		_transformer = transformer;

		_notify = collection as INotifyCollectionChanged;

		if (_notify != null)
			_notify.CollectionChanged += OnCollectionChanged;
	}

	private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		var args = e.Action switch
		           {
			           NotifyCollectionChangedAction.Add => new NotifyCollectionChangedEventArgs(
				           e.Action, e.NewItems.Cast<TSource>().Select(_transformer).ToList(),
				           RecalculateIndex(e.NewStartingIndex)),
			           NotifyCollectionChangedAction.Remove => new NotifyCollectionChangedEventArgs(
				           e.Action, e.OldItems.Cast<TSource>().Select(_transformer).ToList(),
				           RecalculateIndex(e.OldStartingIndex)),
			           NotifyCollectionChangedAction.Replace => new NotifyCollectionChangedEventArgs(
				           e.Action, e.NewItems.Cast<TSource>().Select(_transformer).ToList(),
				           e.OldItems.Cast<TSource>().Select(_transformer).ToList(),
				           RecalculateIndex(e.OldStartingIndex)),
			           NotifyCollectionChangedAction.Move => throw new InvalidOperationException(),
			           NotifyCollectionChangedAction.Reset => e,
			           _ => throw new ArgumentOutOfRangeException()
		           };

		OnCollectionChanged(args);
	}

	private int RecalculateIndex(int idx)
	{
		return _collection.Take(idx).Sum(x => _transformer(x).Count());
	}

	private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
	{
		if (CollectionChanged != null)
			CollectionChanged(this, e);
	}
	public event NotifyCollectionChangedEventHandler? CollectionChanged;
	public void Dispose()
	{
		if (_notify != null)
			_notify.CollectionChanged -= OnCollectionChanged;
	}

	public IEnumerator<TTarget> GetEnumerator()
	{
		return new TransformingEnumerator<TSource, TTarget>(_collection.GetEnumerator(), _transformer);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}