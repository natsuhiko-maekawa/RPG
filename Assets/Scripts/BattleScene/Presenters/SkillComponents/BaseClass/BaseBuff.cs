using BattleScene.Domain.Codes;

namespace BattleScene.Presenters.SkillComponents.BaseClass
{
    public abstract class BaseBuff
    {
        public abstract BuffCode BuffCode { get; }
        public virtual float Rate { get; } = 1.0f;
        public abstract int Turn { get; }
        public abstract LifetimeCode LifetimeCode { get; }
    }
}