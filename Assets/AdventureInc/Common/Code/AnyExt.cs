using System;

namespace AdventureInc
{
    public static class AnyExt
    {
        public static void Do<T>(this T item, Action<T> action)
        {
            action(item);
        }
    }
}