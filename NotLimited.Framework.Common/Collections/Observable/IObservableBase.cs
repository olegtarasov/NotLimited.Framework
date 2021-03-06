﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace NotLimited.Framework.Common.Collections.Observable
{
	public interface IObservableBase<T> : ICollection<T>, INotifyPropertyChanged, INotifyCollectionChanged
	{
	}
}