using System.Collections.Generic;
using System.Linq;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service.DebugService
{
    public class AlwaysTrueRandomService : IMyRandomService
    {
        public T Choice<T>(IEnumerable<T> options)
        {
            return options.First();
        }

        public T Choice<T>(IEnumerable<T> options, long seed)
        {
            return options.First();
        }

        public bool Probability(float rate)
        {
            return true;
        }

        public int Range(int min, int max)
        {
            return min;
        }
    }
}