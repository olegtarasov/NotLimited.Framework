using System.Collections.Specialized;

namespace NotLimited.Framework.Common.Collections.Observable;

/// <summary>
/// Observable list.
/// </summary>
public class ObservableList<T> : ObservableBase<T>, IList<T>
{
	private IList<T> BaseList => (IList<T>)WrappedCollection;

	/// <summary>
	/// Ctor.
	/// </summary>
	public ObservableList() : base(new List<T>())
	{
	}

	/// <summary>
	/// Ctor.
	/// </summary>
	public ObservableList(IList<T> wrappedCollection) : base(wrappedCollection)
	{
	}

	/// <inheritdoc cref="IList{T}.IndexOf" />
	public override int IndexOf(T item)
	{
		return BaseList.IndexOf(item);
	}

	/// <inheritdoc />
	public virtual void Insert(int index, T item)
	{
		CheckReentrancy();
		BaseList.Insert(index, item);
		NotifyProps();
		OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
	}

	/// <inheritdoc />
	public virtual void RemoveAt(int index)
	{
		CheckReentrancy();

		var item = BaseList[index];
		BaseList.RemoveAt(index);
		CheckReentrancy();
		OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
	}

	/// <inheritdoc />
	public T this[int index]
	{
		get => BaseList[index];
		set
		{
			CheckReentrancy();

			var oldItem = BaseList[index];

			BaseList[index] = value;
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
		}
	}

	/// <inheritdoc />
	public override bool Remove(T item)
	{
		CheckReentrancy();
		int idx = BaseList.IndexOf(item);
		bool result = WrappedCollection.Remove(item);

		if (result)
		{
			NotifyProps();
			OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, idx);
		}

		return result;
	}
}