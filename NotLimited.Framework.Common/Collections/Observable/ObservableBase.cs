using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace NotLimited.Framework.Common.Collections.Observable;

/// <summary>
/// Base class for observable collections.
/// </summary>
public abstract class ObservableBase<T> : IObservableCollection<T>
{
	/// <summary>
	/// Underlying collection.
	/// </summary>
	protected readonly ICollection<T> WrappedCollection;

	private int _reeentrancyCount = 0;
		
	#region Notification stuff

	/// <inheritdoc />
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <inheritdoc />
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	/// <summary>
	/// Raises a <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="name">The name of changed property.</param>
	protected virtual void OnPropertyChanged(string name)
	{
		if (PropertyChanged != null)
			PropertyChanged(this, new PropertyChangedEventArgs(name));
	}

	/// <summary>
	/// Raises a <see cref="CollectionChanged"/> event.
	/// </summary>
	/// <param name="args">Collection changed args.</param>
	protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
	{
		if (CollectionChanged != null)
		{
			IncrementReentrancyCount();
			CollectionChanged(this, args);
		}
	}

	/// <summary>
	/// Fired when collection has changed.
	/// </summary>
	/// <param name="action">Type of change.</param>
	/// <param name="item">Item that changed.</param>
	protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, object? item)
	{
		OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item));
	}

	/// <summary>
	/// Fired when collection has changed providing the index of change.
	/// </summary>
	/// <param name="action">Type of change.</param>
	/// <param name="item">Item that changed.</param>
	/// <param name="index">Index where the change happended.</param>
	protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, object? item, int index)
	{
		OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
	}

	/// <summary>
	/// Fired when collection was reset.
	/// </summary>
	protected void OnCollectionReset()
	{
		OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
	}

	/// <summary>
	/// Raises <see cref="PropertyChanged"/> event for <see cref="Count"/> and indexer property.
	/// </summary>
	protected void NotifyProps()
	{
		OnPropertyChanged("Count");
		OnPropertyChanged("Item[]");
	}

	#endregion

	/// <summary>
	/// Ctor.
	/// </summary>
	protected ObservableBase(ICollection<T> wrappedCollection)
	{
		WrappedCollection = wrappedCollection ?? throw new ArgumentException(nameof(wrappedCollection));
	}

	/// <inheritdoc />
	public virtual IEnumerator<T> GetEnumerator()
	{
		return WrappedCollection.GetEnumerator();
	}

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <inheritdoc />
	public virtual void Add(T item)
	{
		CheckReentrancy();
		WrappedCollection.Add(item);
		NotifyProps();
		OnCollectionChanged(NotifyCollectionChangedAction.Add, item, WrappedCollection.Count - 1);
	}

	/// <inheritdoc />
	public virtual void Clear()
	{
		CheckReentrancy();
		WrappedCollection.Clear();
		NotifyProps();
		OnCollectionReset();
	}

	/// <inheritdoc />
	public virtual bool Contains(T item)
	{
		return WrappedCollection.Contains(item);
	}

	/// <inheritdoc />
	public void CopyTo(T[] array, int arrayIndex)
	{
		WrappedCollection.CopyTo(array, arrayIndex);
	}

	/// <inheritdoc />
	public virtual bool Remove(T item)
	{
		CheckReentrancy();
		int idx = IndexOf(item);

		if (idx == -1)
			return false;

		bool result = WrappedCollection.Remove(item);

		if (!result)
			return false;

		NotifyProps();
		OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, idx);

		return true;
	}

	/// <inheritdoc />
	public int Count => WrappedCollection.Count;

	/// <inheritdoc />
	public bool IsReadOnly => WrappedCollection.IsReadOnly;

	/// <summary>
	/// Returns the index of specified element, or -1 if the elements is not in collection.
	/// </summary>
	/// <param name="item">Item to find.</param>
	/// <returns>The index of specified element, or -1 if the elements is not in collection.</returns>
	public virtual int IndexOf(T item)
	{
		int idx = 0;

		foreach (var element in WrappedCollection)
		{
			if (Equals(element, item))
				return idx;
			idx++;
		}

		return -1;
	}

	#region Reentrancy

	private void IncrementReentrancyCount()
	{
		_reeentrancyCount++;
	}

	/// <summary>
	/// Checks whether a method was called inside a change event handler.
	/// </summary>
	protected void CheckReentrancy()
	{
		if (_reeentrancyCount > 0 && CollectionChanged != null && CollectionChanged.GetInvocationList().Length > 1)
		{
			throw new InvalidOperationException("You can't modify the collection inside change event handler");
		}
	}

	#endregion
}