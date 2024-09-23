using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.PrimeSkill.BaseClass
{
    public abstract class BaseDestroy
    {
        public abstract BodyPartCode BodyPartCode { get; }
        public virtual float LuckRate { get; } = 0.5f;
        public virtual int Count { get; } = 1;
    }
}