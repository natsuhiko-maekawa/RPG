using System.Collections.Generic;
using NUnit.Framework;
using Utility;

namespace Tests.Utility
{
    [TestFixture]
    public class ListExTest
    {
        private readonly List<int> _singleNumber = new() { 0 };
        private readonly List<int> _threeNumbers = new() { 0, 1, 2 };
        private readonly List<int> _threeNumbersTwoSame = new() { 0, 1, 1 };
        private readonly List<int> _threeNumbersAllSame = new() { 0, 0, 0 };

        [Test]
        public void 一つの選択肢から一つの要素からなる組み合わせを取得()
        {
            var expected = _singleNumber.CombinationTest(1, 1);
            var actual = _singleNumber.Combination(1, 1);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void 一つの選択肢から三つの要素からなる組み合わせを取得()
        {
            var expected = _singleNumber.CombinationTest(1, 3);
            var actual = _singleNumber.Combination(1, 3);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void 三つの選択肢から一つの要素からなる組み合わせを取得()
        {
            var expected = _threeNumbers.CombinationTest(1, 1);
            var actual = _threeNumbers.Combination(1, 1);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void 三つの選択肢から三つの要素からなる組み合わせを取得()
        {
            var expected = _threeNumbers.CombinationTest(1, 3);
            var actual = _threeNumbers.Combination(1, 3);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void 三つのうち二つが同じ選択肢から一つの要素からなる組み合わせを取得()
        {
            var expected = _threeNumbersTwoSame.CombinationTest(1, 1);
            var actual = _threeNumbersTwoSame.Combination(1, 1);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void 三つのうち二つが同じ選択肢から三つの要素からなる組み合わせを取得()
        {
            var expected = _threeNumbersTwoSame.CombinationTest(1, 3);
            var actual = _threeNumbersTwoSame.Combination(1, 3);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void 三つのうちすべてが同じ選択肢から一つの要素からなる組み合わせを取得()
        {
            var expected = _threeNumbersAllSame.CombinationTest(1, 1);
            var actual = _threeNumbersAllSame.Combination(1, 1);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void 三つのうちすべてが同じ選択肢から三つの要素からなる組み合わせを取得()
        {
            var expected = _threeNumbersAllSame.CombinationTest(1, 3);
            var actual = _threeNumbersAllSame.Combination(1, 3);
            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}