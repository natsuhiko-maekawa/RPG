using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.PrimeSkill.BaseClass
{
    public abstract class BaseAilment
    {
        public abstract AilmentCode AilmentCode { get; }
        public virtual float LuckRate { get; } = 0.5f;
    }
}