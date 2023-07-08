using System.Collections.Generic;

namespace GMTK2023
{
    // ReSharper disable once InconsistentNaming
    public static class IDictionaryExt
    {
        public static TValue? TryGet<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key) =>
            dict.TryGetValue(key, out var value)
                ? value
                : default;
    }
}