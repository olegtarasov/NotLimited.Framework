using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NotLimited.Framework.Common.Collections
{
    public class ConcurrentDictionaryReadonly<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys { get { return Keys; } }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values { get { return Values; } }
    }
}