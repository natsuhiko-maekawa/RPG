using System.Collections.Generic;

namespace BattleScene.UseCases.IService
{
    public interface IMyRandomService
    {
        public T Choice<T>(IEnumerable<T> options);
        public T Choice<T>(IEnumerable<T> options, long seed);
        public bool Probability(float rate);
        // public bool Probability(float rate, long seed);
        public int Range(int min, int max);
        // public int Range(int min, int max, long seed);
    }
}