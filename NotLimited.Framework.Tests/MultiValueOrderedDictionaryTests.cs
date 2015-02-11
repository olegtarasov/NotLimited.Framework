using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotLimited.Framework.Collections;
using Xunit;

namespace NotLimited.Framework.Tests
{
    public class MultiValueOrderedDictionaryTests
    {
        [Fact]
        public void Test()
        {
            var dic = new OrderedMultiValueDictionary<int, string>();
            dic.Add(42, "ojefjif");
            bool t1 = dic.ContainsKey(42);
            dic.Remove(42);
            bool t2 = dic.ContainsKey(42);
        }
    }
}
