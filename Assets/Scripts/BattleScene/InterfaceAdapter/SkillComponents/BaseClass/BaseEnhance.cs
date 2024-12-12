using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.SkillComponents.BaseClass
{
    public abstract class BaseEnhance
    {
        public abstract EnhanceCode EnhanceCode { get; }
        public abstract int Turn { get; }
        public abstract LifetimeCode LifetimeCode { get; }
    }
}