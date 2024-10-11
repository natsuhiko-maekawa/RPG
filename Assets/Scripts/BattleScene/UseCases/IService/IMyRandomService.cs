using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BattleScene.UseCases.IService
{
    public interface IMyRandomService
    {
        public T Choice<T>(IEnumerable<T> options, [CallerMemberName] string memberName = "");
        public T Choice<T>(IEnumerable<T> options, long seed, [CallerMemberName] string memberName = "");
        public bool Probability(float rate, [CallerMemberName] string memberName = "");
        // public bool Probability(float rate, long seed, [CallerMemberName] string memberName = "");
        public int Range(int min, int max, [CallerMemberName] string memberName = "");
        // public int Range(int min, int max, long seed, [CallerMemberName] string memberName = "");
    }
}