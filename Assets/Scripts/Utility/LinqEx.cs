using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Utility
{
    public static class LinqEx
    {
        public static IEnumerable<T[]> Combination<T>(this IEnumerable<T> source, int k1, int k2)
        {
            var distinctSourceArray = source.Distinct().ToArray();
            var combination = new List<T[]>(); 
            for (var k = k1; k < k2 + 1; ++k)
            {
                CreateCombination(k, distinctSourceArray.Length, distinctSourceArray, combination, 0, Span<int>.Empty);
            }

            return combination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CreateCombination<T>(int k, int n, T[] source, List<T[]> combination,
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