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

        public T Choice<T>(IEnumerable<T> options, string memberName = "")
        {
            throw new System.NotImplementedException();
        }

        public T Choice<T>(IEnumerable<T> options, long seed, string memberName = "")
        {
            throw new System.NotImplementedException();
        }

        public bool Probability(float rate, string memberName = "")
        {
            throw new System.NotImplementedException();
        }

        public int Range(int min, int max, string memberName = "")
        {
            throw new System.NotImplementedException();
        }
    }
}