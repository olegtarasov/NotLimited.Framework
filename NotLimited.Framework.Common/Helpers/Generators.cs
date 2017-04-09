using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NotLimited.Framework.Common.Helpers
{
    public static class Generators
    {
        public static Dictionary<TKey, TValue> dict<TKey, TValue, TSrc>(
            IEnumerable<TSrc> source, Func<TSrc, TKey> keyFunc, Func<TSrc, TValue> valueFunc)
        {
            return source.ToDictionary(keyFunc, valueFunc);
        }

        public static Dictionary<TKey, TValue> dict<TKey, TValue>()
        {
            return new Dictionary<TKey, TValue>();
        }

        public static FileStream ofstream(string path, FileMode mode = FileMode.Create)
        {
            return new FileStream(path, mode, FileAccess.Write);
        }

        public static StreamWriter swriter(string path)
        {
            return new StreamWriter(path);
        }
    }
}