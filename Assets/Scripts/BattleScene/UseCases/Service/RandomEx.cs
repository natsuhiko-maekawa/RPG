using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Interface;
using Random = Unity.Mathematics.Random;

namespace Utility
{
    public class RandomEx : IRandomEx
    {
        public T Choice<T>(IEnumerable<T> options)
        {
            return Choice(options, GenerateSeed());
        }

        public T Choice<T>(IEnumerable<T> options, long seed)
        {
            return Choice(options, (uint)seed);
        }

        public bool Probability(float rate)
        {
            return Probability(rate, GenerateSeed());
        }

        public bool Probability(float rate, long seed)
        {
            return Probability(rate, (uint)seed);
        }

        public int Range(int min, int max)
        {
            return Range(min, max, GenerateSeed());
        }

        public int Range(int min, int max, long seed)
        {
            return Range(min, max, (uint)seed);
        }

        private T Choice<T>(IEnumerable<T> options, uint seed)
        {
            var optionList = options.ToList();
            var random = new Random(seed);
            var index = random.NextInt(optionList.Count);
            return optionList[index];
        }

        private bool Probability(float rate, uint seed)
        {
            var random = new Random(seed);
            return rate >= random.NextFloat(1.0f);
        }

        private int Range(int min, int max, uint seed)
        {
            var random = new Random(seed);
            return random.NextInt(min, max);
        }

        private uint GenerateSeed()
        {
            var intSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            return BitConverter.ToUInt32(BitConverter.GetBytes(intSeed));
        }
    }
}