using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace NotLimited.Framework.Common.Collections.Observable
{
	[DataContract]
	public class ObservableBase<T> : IObservableBase<T>, IDisposable
	{
		[DataMember]
		protected ICollection<T> baseCollection;

		[DataMember]
		private readonly SimpleMonitor monitor = new SimpleMonitor();

		#region Notification stuff

		public event PropertyChangedEventHandler PropertyChanged;
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		protected virtual void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			if (CollectionChanged != null)
				using (BlockReentrancy())
					CollectionChanged(this, args);
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, object item)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item));
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		protected void OnCollectionReset()
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		protected void NotifyProps()
		{
			OnPropertyChanged("Count");
			OnPropertyChanged("Item[]");
		}

		#endregion

		#region Forced notifications

		public void RaiseAdd()
		{
			OnCollectionChanged(NotifyCollectionChangedAction.Add, baseCollection.Last(), baseCollection.Count - 1);
		}

		public void RaiseRemove(T item, int idx)
		{
			OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, idx);
		}

		#endregion

		public ObservableBase(ICollection<T> baseCollection)
		{
			if (baseCollection == null)
				throw new ArgumentException("baseCollection");

			this.baseCollection = baseCollection;
		}

		public virtual IEnumerator<T> GetEnumerator()
		{
			return baseCollection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public virtual void Add(T item)
		{
			CheckReentrancy();
			baseCollection.Add(item);
			NotifyProps();
			OnCollectionChanged(NotifyCollectionChangedAction.Add, item, baseCollection.Count - 1);
		}

		public virtual void Clear()
		{
			CheckReentrancy();
			baseCollection.Clear();
			NotifyProps();
			OnCollectionReset();
		}

		public virtual bool Contains(T item)
		{
			return baseCollection.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			baseCollection.CopyTo(array, arrayIndex);
		}

		public virtual bool Remove(T item)
		{
			CheckReentrancy();
			int idx = IndexOf(item);

			if (idx == -1)
				return false;

			bool result = baseCollection.Remove(item);

			if (!result)
				return false;

			NotifyProps();
			OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, idx);

			return true;
		}

		public int Count
		{
			get { return baseCollection.Count; }
		}

		public bool IsReadOnly
		{
			get { return baseCollection.IsReadOnly; }
		}

		public virtual int IndexOf(T item)
		{
			int idx = 0;

			foreach (var element in baseCollection)
			{
				if (element.Equals(item))
					return idx;
				idx++;
			}

			return -1;
		}

		#region Reentrancy

		protected IDisposable BlockReentrancy()
		{
			monitor.Enter();
			return monitor;
		}

		protected void CheckReentrancy()
		{
			if ((monitor.Busy && (CollectionChanged != null)) && (CollectionChanged.GetInvocationList().Length > 1))
			{
				throw new InvalidOperationException("Collection Reentrancy Not Allowed");
			}
		}

		#endregion

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				monitor.Dispose();
			}
		}
	}
}