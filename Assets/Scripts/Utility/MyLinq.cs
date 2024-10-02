using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    // https://baba-s.hatenablog.com/entry/2019/12/04/125000
    public static class MyLinq
    {
        public static IEnumerable<List<T>> Combination<T>(this IEnumerable<T> self, int n)
        {
            var list = self.ToList();
            return Enumerable.Range(0, n - 1)
                .Aggregate(
                    Enumerable.Range(0, list.Count)
                        .Select(index => new List<int> { index })
                    , (x, _) => x
                        .SelectMany(indices => Enumerable.Range(indices.Max(), list.Count - indices.Max())
                            .Select(index => new List<int>(indices) { index })))
                .Select(indices => indices
                    .Select(index => list[index])
                    .ToList());
        }

        public static IEnumerable<List<T>> Combination<T>(this IEnumerable<T> self, int m, int n)
        {
            return Enumerable.Range(m, n)
                .SelectMany(x => self.Combination(x));
        }
    }
}