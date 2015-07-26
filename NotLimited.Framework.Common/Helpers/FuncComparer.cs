using System;
using System.Collections.Generic;

namespace NotLimited.Framework.Common.Helpers
{
    public class FuncComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _equalityFunc;
        private readonly Func<T, int> _hashFunc;

	    public FuncComparer(Func<T, T, bool> equalityFunc, Func<T, int> hashFunc)
        {
            _equalityFunc = equalityFunc;
            _hashFunc = hashFunc;
        }

        public bool Equals(T x, T y)
        {
            return _equalityFunc(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _hashFunc(obj);
        }
    }
}