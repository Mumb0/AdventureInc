using System;
using System.Collections.Generic;

namespace GMTK2023
{
    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExt
    {
        public static void Iter<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}