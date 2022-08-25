using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace NotLimited.Framework.Common.Collections.Observable;

/// <summary>
/// Observable collection interface.
/// </summary>
public interface IObservableCollection<T> : ICollection<T>, INotifyPropertyChanged, INotifyCollectionChanged
{
}