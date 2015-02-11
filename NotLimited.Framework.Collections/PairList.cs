using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NotLimited.Framework.Collections
{
    public class Pair<TKey, TValue>
    {
        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }

    public class PairList<TKey, TValue> : List<Pair<TKey, TValue>>
    {
        public PairList()
        {
        }

        public PairList(IEnumerable<Pair<TKey, TValue>> collection) : base(collection)
        {
        }

        public void Add(TKey key, TValue value)
        {
            Add(new Pair<TKey, TValue>(key, value));
        }

        public bool ContainsKey(TKey key)
        {
            return this.Any(x => Equals(x.Key, key));
        }

        public TValue this[TKey key]
        {
            get { return this.First(x => Equals(x.Key, key)).Value; }
            set
            {
                var pair = this.FirstOrDefault(x => Equals(x.Key, key));
                if (pair != null)
                {
                    Remove(pair);
                }

                Add(key, value);
            }
        }
    }
}