using System;
using System.Collections.Generic;
using System.IO;
using System.Web.WebPages;

namespace NotLimited.Framework.Web.Mvc
{
    public class AggregateHelperResult
    {
        private readonly List<HelperResult> _results = new List<HelperResult>();

        public void Add(HelperResult result)
        {
            _results.Add(result);
        }

        public static implicit operator Action<TextWriter>(AggregateHelperResult result)
        {
            return writer =>
            {
                foreach (var helperResult in result._results)
                {
                    helperResult.WriteTo(writer);
                }
            };
        }
    }
}