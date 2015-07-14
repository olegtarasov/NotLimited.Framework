using System;

namespace NotLimited.Framework.Common.Helpers
{
    public static class DelegateExtensions
    {
        public static void InvokeIfNotNull<T>(this Action<T> action, T arg1)
        {
            if (action != null)
            {
                action(arg1);
            }
        }

        public static void InvokeIfNotNull<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (action != null)
            {
                action(arg1, arg2);
            }
        }
    }
}