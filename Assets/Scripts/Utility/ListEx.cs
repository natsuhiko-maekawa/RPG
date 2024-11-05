using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    public static class ListEx
    {
        public static List<List<T>> Combination<T>(this IReadOnlyList<T> source, int k1, int k2)
        {
            var indexCombination = new List<List<int>>();
            for (var k = k1; k < k2 + 1; ++k)
            {
                var tmpIndexCombination = CreateIndexCombination(source.Count, k);
                indexCombination.AddRange(tmpIndexCombination);
            }

            var combination = indexCombination
                .Select(subset => subset
                    .Select(i => source[i])
                    .ToList())
                .Distinct(new SequenceEqualityComparer<T>())
                .ToList();
            return combination;
        }

        private static List<List<int>> CreateIndexCombination(int n, int k, int i = 0, List<int>? subset = null, List<List<int>>? combination = null)
        {
            subset ??= new List<int>();
            combination ??= new List<List<int>>();
            if (subset.Count < k && i < n)
            {
                var newSubset1 = new List<int>(subset) { i };
                CreateIndexCombination(n: n, k: k, i: i, subset: newSubset1, combination: combination);
                var newSubset2 = new List<int>(subset) { i };
                CreateIndexCombination(n: n, k: k, i: i + 1, subset: newSubset2, combination: combination);
                CreateIndexCombination(n: n, k: k, i: i + 1, subset: subset, combination: combination);
            }
            else
            {
                if (subset.Count != 0) combination.Add(subset);
            }

            return combination;
        }

        class SequenceEqualityComparer<T> : EqualityComparer<List<T>>
        {
            public override bool Equals(List<T> x, List<T> y) => x.SequenceEqual(y);
            public override int GetHashCode(List<T> obj) => 0;
        }
    }
}