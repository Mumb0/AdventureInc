using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

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

        public static void Iter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items, Action<TKey, TValue> action)
        {
            foreach (var item in items)
            {
                action(item.Key, item.Value);
            }
        }

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> items, Func<T, bool> pred) =>
            items.Where(it => !pred(it));

        public static T? TryRandom<T>(this IReadOnlyCollection<T> items)
        {
            if (!items.Any()) return default;

            var index = Random.Range(0, items.Count);
            return items.ElementAt(index);
        }

        public static T? TryWeightedRandom<T>(this IReadOnlyCollection<T> items, Func<T, float> weightSelector)
        {
            var totalWeight = items.Sum(weightSelector);
            var itemWeightIndex = Random.Range(0, totalWeight);
            float currentWeightIndex = 0;

            foreach (var item in from weightedItem in items select new {Value = weightedItem, Weight = weightSelector(weightedItem)})
            {
                currentWeightIndex += item.Weight;

                // If we've hit or passed the weight we are after for this item then it's the one we want....
                if (currentWeightIndex >= itemWeightIndex)
                    return item.Value;
            }

            return default;
        }

        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> items, T exclude) =>
            items.Except(exclude.Yield());

        public static IEnumerable<TMapped> TrySelect<T, TMapped>(this IEnumerable<T> items, Func<T, TMapped?> map)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var item in items)
            {
                var mapped = map(item);
                if (mapped != null) yield return mapped;
            }
        }
    }
}