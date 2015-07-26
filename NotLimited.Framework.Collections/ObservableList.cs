using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace NotLimited.Framework.Collections
{
	[DataContract]
	public class ObservableList<T> : ObservableBase<T>, IList<T>
	{
		private IList<T> BaseList { get { return (IList<T>)baseCollection; } }

		public ObservableList() : base(new List<T>())
		{
		}

		public ObservableList(IList<T> baseCollection) : base(baseCollection)
		{
		}

		public override int IndexOf(T item)
		{
			return BaseList.IndexOf(item);
		}

		public virtual void Insert(int index, T item)
		{
			CheckReentrancy();
			BaseList.Insert(index, item);
			NotifyProps();
			OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		public virtual void RemoveAt(int index)
		{
			CheckReentrancy();

			var item = BaseList[index];
			BaseList.RemoveAt(index);
			CheckReentrancy();
			OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
		}

		public T this[int index]
		{
			get { return BaseList[index]; }
			set
			{
				CheckReentrancy();

				var oldItem = BaseList[index];

				BaseList[index] = value;
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
			}
		}

		public override bool Remove(T item)
		{
			CheckReentrancy();
			int idx = BaseList.IndexOf(item);
			bool result = baseCollection.Remove(item);

			if (result)
			{
				NotifyProps();
				OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, idx);
			}

			return result;
		}
	}
}