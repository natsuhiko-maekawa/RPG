using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.Skill.AbstractClass
{
    public abstract class AbstractSlipDamage
    {
        public abstract SlipDamageCode SlipDamageCode { get; }
        public virtual float DamageRate { get; } = 1.2f;
        public virtual float LuckRate { get; } = 0.5f;
    }
}