using System;
using System.Collections;
using System.Collections.Generic;

namespace NotLimited.Framework.Collections
{
    public class LooseListNode<T>
    {
        public LooseListNode(T value, LooseListNode<T> next, LooseListNode<T> prev)
        {
            Value = value;
            Next = next;
            Prev = prev;
        }

        public T Value;
        public LooseListNode<T> Next;
        public LooseListNode<T> Prev;
    }

    /// <summary>
    /// This is a linked list which supports node removeal while enumerating.
    /// </summary>
    public class LooseList<T> : IEnumerable<T>
    {
        private class LooseEnumerator : IEnumerator<T>
        {
            private LooseListNode<T> _cur;
            private readonly LooseList<T> _list;

            public LooseEnumerator(LooseList<T> list)
            {
                _list = list;
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
            }

            #endregion

            #region Implementation of IEnumerator

            public bool MoveNext()
            {
                if (_cur == null)
                {
                    if (_list._first == null)
                        return false;

                    _cur = _list._first;
                    return true;
                }

                if (_cur.Next == null)
                    return false;

                _cur = _cur.Next;
                return true;
            }

            public void Reset()
            {
                _cur = null;
            }

            public T Current { get { return _cur == null ? default(T) : _cur.Value; } }

            object IEnumerator.Current { get { return Current; } }

            #endregion
        }

        private LooseListNode<T> _first;
        private LooseListNode<T> _last;

        public void AddFirst(T item)
        {
            _first = new LooseListNode<T>(item, _first, null);
            if (_last == null)
            {
                _last = _first;
            }
        }

        public void AddLast(T item)
        {
            _last = new LooseListNode<T>(item, null, _last);
            if (_first == null)
            {
                _first = _last;
            }
        }

        public bool Remove(T item)
        {
            if (_first == null || _last == null || item == null)
                return false;

            if (item.Equals(_first.Value))
            {
                _first = _first.Next;
                if (_first == null)
                {
                    _last = null;
                }

                return true;
            }

            if (item.Equals(_last.Value))
            {
                _last = _last.Prev;
                if (_last == null)
                {
                    _first = null;
                }

                return true;
            }

            var cur = _first;
            while (cur.Next != null)
            {
                if (item.Equals(cur.Next.Value))
                {
                    cur.Next = cur.Next.Next;
                    cur.Next.Prev = cur;
                    break;
                }
                cur = cur.Next;
            }

            return true;
        }

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new LooseEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}