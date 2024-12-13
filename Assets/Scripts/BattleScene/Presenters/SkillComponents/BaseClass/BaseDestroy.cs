using BattleScene.Domain.Codes;

namespace BattleScene.Presenters.SkillComponents.BaseClass
{
    public abstract class BaseDestroy
    {
        public abstract BodyPartCode BodyPartCode { get; }
        public virtual float LuckRate { get; } = 0.5f;
        public virtual int Count { get; } = 1;
    }
}