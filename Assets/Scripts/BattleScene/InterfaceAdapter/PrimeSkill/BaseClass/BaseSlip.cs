using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.PrimeSkill.BaseClass
{
    public abstract class BaseSlip
    {
        public abstract SlipCode SlipCode { get; }
        public virtual float DamageRate { get; } = 1.2f;
        public virtual float LuckRate { get; } = 0.5f;
    }
}