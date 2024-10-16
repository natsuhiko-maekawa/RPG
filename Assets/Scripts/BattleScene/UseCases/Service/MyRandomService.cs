using System.Collections.Generic;
using BattleScene.UseCases.IService;
using Utility;

namespace BattleScene.UseCases.Service
{
    public class MyRandomService : IMyRandomService
    {
        public T Choice<T>(IEnumerable<T> options, string memberName = "") => MyRandom.Choice(options);
        public T Choice<T>(IEnumerable<T> options, long seed, string memberName = "") => MyRandom.Choice(options, seed);
        public bool Probability(float rate, string memberName = "") => MyRandom.Probability(rate);
        public int Range(int min, int max, string memberName = "") => MyRandom.Range(min, max);
    }
}