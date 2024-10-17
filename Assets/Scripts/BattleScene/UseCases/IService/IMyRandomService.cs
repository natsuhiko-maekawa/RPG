using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BattleScene.UseCases.IService
{
    /// <summary>
    /// UnityのRandomクラスの拡張クラスを定義するインタフェース。<br/>
    /// <see cref="BattleScene.UseCases.Service.DebugService.DebugRandomService"/>のために、
    /// 各メソッドにCallerMemberName属性を付与した省略可能引数がある。この引数はMyRandomServiceでは使用しない。
    /// </summary>
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