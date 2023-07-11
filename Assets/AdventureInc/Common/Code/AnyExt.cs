using System;

namespace GMTK2023
{
    public static class AnyExt
    {
        public static void Do<T>(this T item, Action<T> action)
        {
            action(item);
        }
    }
}