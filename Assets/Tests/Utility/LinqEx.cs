using System.Collections.Generic;
using System.Linq;

namespace Tests.Utility
{
    public static class LinqEx
    {
        /// <summary>
        /// リストの要素の組み合わせを求める拡張メソッドのテスト版。<br/>
        /// <see href="https://baba-s.hatenablog.com/entry/2019/12/04/125000">このサイト</see>を参考にLINQを用いて実装した。
        /// </summary>
        /// <param name="source">要素の集まり。</param>
        /// <param name="k1">選ぶ個数の最小。</param>
        /// <param name="k2">選ぶ個数の最大。</param>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <returns>リストの要素の組み合わせのリスト。</returns>
        // このLINQを用いたメソッドをLinqCombination、再帰を用いた実際のCombinationメソッドをRecursiveCombinationとして、
        // ベンチマークテストを行った。再帰を用いた実際のCombinationメソッドの方がパフォーマンスが良い。
        // | Method               | Mean       | Error       | StdDev     | Gen0    | Gen1   | Allocated |
        // |--------------------- |-----------:|------------:|-----------:|--------:|-------:|----------:|
        // | LinqCombination      | 835.067 us | 279.1580 us | 15.3016 us | 18.5547 | 1.9531 | 115.73 KB |
        // | RecursiveCombination |   9.894 us |   0.4065 us |  0.0223 us |  3.4943 | 0.2136 |  21.49 KB |
        public static List<List<T>> CombinationTest<T>(this IReadOnlyList<T> source, int k1, int k2)
        {
            var combination = new List<List<T>>();
            for (var k = k1; k < k2 + 1; ++k)
            {
                // source.Count=3としたとき、accumulator = [[0], [1], [2]]
                var accumulator = Enumerable.Range(0, source.Count)
                    .Select(index => new List<int> { index });
                // k=3としたとき、[0, 1]
                var tmpCombination = Enumerable.Range(0, k - 1)
                    .Aggregate(accumulator, (a, _) => a.SelectMany(indices => 
                                // indices=[0]のとき、[0, 1, 2]
                                // indices=[1]のとき、[1, 2]
                                // indices=[2]のとき、[2]
                                // indices=[0, 0]のとき、[0, 1, 2]...
                            Enumerable.Range(indices.Max(), source.Count - indices.Max())
                                // indices=[0]、x=0のとき、[0, 0]
                                // indices=[0]、x=1のとき、[0, 1]
                                // indices=[0]、x=2のとき、[0, 2]
                                // indices=[1]、x=1のとき、[1, 1]
                                // indices=[1]、x=2のとき、[1, 2]
                                // indices=[2]、x=2のとき、[2, 2]
                                // indices=[0, 0]のとき、[0, 0, 0]...
                            .Select(index => new List<int>(indices) { index })))
                    .Select(indices => indices
                        .Select(index => source[index])
                        .ToList())
                    .Distinct(new SequenceEqualityComparer<T>())
                    .ToList();
                combination.AddRange(tmpCombination);
            }

            return combination;
        }

        private class SequenceEqualityComparer<T> : EqualityComparer<List<T>>
        {
            public override bool Equals(List<T> x, List<T> y) => x.SequenceEqual(y);
            public override int GetHashCode(List<T> obj) => 0;
        }
    }
}