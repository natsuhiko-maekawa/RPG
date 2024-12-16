using BattleScene.Domain.Codes;

namespace BattleScene.Presenters.SkillComponents.BaseClass
{
    public abstract class BaseEnhance
    {
        public abstract EnhanceCode EnhanceCode { get; }
        public abstract int Turn { get; }
        public abstract LifetimeCode LifetimeCode { get; }
    }
}