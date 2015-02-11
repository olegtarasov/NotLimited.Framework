using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NotLimited.Framework.Collections
{
    public class OrderedMultiValueDictionary<TKey, TValue> : MultiValueDictionary<TKey, TValue>, IEnumerable
    {
         private readonly List<TKey> _order = new List<TKey>();

        public new void Add(TKey key, TValue value)
        {
            if (!ContainsKey(key))
            {
                _order.Add(key);
            }

            base.Add(key, value);
        }

        public new void AddRange(TKey key, IEnumerable<TValue> values)
        {
            if (!ContainsKey(key))
            {
                _order.Add(key);
            }

            base.AddRange(key, values);
        }

        public new bool Remove(TKey key)
        {
            _order.RemoveAll(x => Equals(x, key));
            return base.Remove(key);
        }

        public new bool Remove(TKey key, TValue value)
        {
            bool result = base.Remove(key, value);
            if (!ContainsKey(key))
            {
                _order.RemoveAll(x => Equals(x, key));
            }

            return result;
        }

        public new IEnumerable<TKey> Keys { get { return _order; } }

        /// <summary>
        /// This property bears a penalty of ordering all the values every time.
        /// </summary>
        public new IEnumerable<IReadOnlyCollection<TValue>> Values
        {
            get { return _order.Select(x => this[x]); }
        }

        public new IEnumerator<KeyValuePair<TKey, IReadOnlyCollection<TValue>>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void Sort()
        {
            _order.Sort();
        }

        public void SortDescending()
        {
            _order.Sort();
            _order.Reverse();
        }

        private class Enumerator : IEnumerator<KeyValuePair<TKey, IReadOnlyCollection<TValue>>>
        {
            private readonly OrderedMultiValueDictionary<TKey, TValue> _dictionary;
            private readonly IEnumerator<TKey> _orderEnumerator;

            public Enumerator(OrderedMultiValueDictionary<TKey, TValue> dictionary)
            {
                _dictionary = dictionary;
                _orderEnumerator = _dictionary._order.GetEnumerator();
            }

            public void Dispose()
            {
                _orderEnumerator.Dispose();
            }

            public bool MoveNext()
            {
                return _orderEnumerator.MoveNext();
            }

            public void Reset()
            {
                _orderEnumerator.Reset();
            }

            public KeyValuePair<TKey, IReadOnlyCollection<TValue>> Current
            {
                get
                {
                    var key = _orderEnumerator.Current;
                    return new KeyValuePair<TKey, IReadOnlyCollection<TValue>>(key, _dictionary[key]);
                }
            }

            object IEnumerator.Current
            {
                get { return Current; } }
        }
    }
}