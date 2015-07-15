using System;
using System.Text;

namespace NotLimited.Framework.Common.Helpers
{
    public static class ExceptionExtensions
    {
        public static string GetMessageHierarchy(this Exception exception)
        {
            var builder = new StringBuilder();
            int cnt = 0;
            foreach (var ex  in exception.IterateQueue(x => x, x => x.InnerException))
            {
                builder.Append(' ', cnt * 2).Append("==> ").Append(ex.GetType().Name).Append(": ").AppendLine(ex.Message);
            }

            return builder.ToString();
        }
    }
}