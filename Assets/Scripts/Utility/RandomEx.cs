using System;
using System.Collections.Generic;
using System.Linq;

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
        
        private T Choice<T>(IEnumerable<T> options, uint seed)
        {
            var optionList = options.ToList();
            var random = new Unity.Mathematics.Random(seed);
            var index = random.NextInt(optionList.Count);
            return optionList[index];
        }

        public bool Probability(float rate)
        {
            return Probability(rate, GenerateSeed());
        }

        public bool Probability(float rate, long seed)
        {
            return Probability(rate, (uint)seed);
        }
        
        private bool Probability(float rate, uint seed)
        {
            var random = new Unity.Mathematics.Random(seed);
            return rate >= random.NextFloat(1.0f);
        }

        public int Range(int min, int max)
        {
            return Range(min, max, GenerateSeed());
        }

        public int Range(int min, int max, long seed)
        {
            return Range(min, max, (uint)seed);
        }
        
        private int Range(int min, int max, uint seed)
        {
            var random = new Unity.Mathematics.Random(seed);
            return random.NextInt(min, max);
        }
        
        private uint GenerateSeed()
        {
            var intSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            return BitConverter.ToUInt32(BitConverter.GetBytes(intSeed));
        }
    }
}