using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.PrimeSkill.BaseClass
{
    public abstract class BaseEnhance
    {
        public abstract EnhanceCode EnhanceCode { get; }
        public abstract int Turn { get; }
        public abstract LifetimeCode LifetimeCode { get; }
    }
}