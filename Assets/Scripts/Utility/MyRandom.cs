﻿using System;
using System.Collections.Generic;
using System.Linq;
using Random = Unity.Mathematics.Random;

namespace Utility
{
    public static class MyRandom
    {
        public static T Choice<T>(IEnumerable<T> options)
            => Choice(options, GenerateSeed());

        public static T Choice<T>(IEnumerable<T> options, long seed)
            => Choice(options, (uint)seed);

        public static bool Probability(float rate)
            => Probability(rate, GenerateSeed());

        public static bool Probability(float rate, long seed)
            => Probability(rate, (uint)seed);

        public static int Range(int min, int max)
            => Range(min, max, GenerateSeed());

        public static int Range(int min, int max, long seed)
            => Range(min, max, (uint)seed);

        private static T Choice<T>(IEnumerable<T> options, uint seed)
        {
            var optionList = options.ToList();
            var random = new Random(seed);
            var index = random.NextInt(optionList.Count);
            var choose = optionList[index];
            return choose;
        }

        private static bool Probability(float rate, uint seed)
        {
            var random = new Random(seed);
            var value = rate >= random.NextFloat(1.0f);
            return value;
        }

        private static int Range(int min, int max, uint seed)
        {
            var random = new Random(seed);
            var value = random.NextInt(min, max);
            return value;
        }

        private static uint GenerateSeed()
        {
            var intSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            var seed = BitConverter.ToUInt32(BitConverter.GetBytes(intSeed));
            return seed;
        }
    }
}