using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Utility
{
    public static class ListEx
    {
        public static IReadOnlyList<IReadOnlyList<T>> Combination<T>(this IReadOnlyList<T> source, int k1, int k2)
        {
            var distinctSourceArray = source.Distinct().ToArray();
            var combination = new List<IReadOnlyList<T>>(); 
            for (var k = k1; k < k2 + 1; ++k)
            {
                CreateCombination(k, distinctSourceArray.Length, distinctSourceArray, combination, 0, Span<int>.Empty);
            }

            return combination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CreateCombination<T>(int k, int n, IReadOnlyList<T> source, List<IReadOnlyList<T>> combination,
            int index, Span<int> indexSubset)
        {
            if (indexSubset.Length < k && index < n)
            {
                Span<int> newSubset = stackalloc int[indexSubset.Length + 1];
                indexSubset.CopyTo(newSubset);
                newSubset[^1] = index;
                CreateCombination(k, n, source, combination, index: index, indexSubset: newSubset);
                CreateCombination(k, n, source, combination, index: index + 1, indexSubset: indexSubset);
            }
            else
            {
                if (indexSubset.Length != k) return;

                var subset = new T[indexSubset.Length];
                for (var i = 0; i < indexSubset.Length; ++i)
                {
                    subset[i] = source[indexSubset[i]];
                }

                combination.Add(subset);
            }
        }
    }
}