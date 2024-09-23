using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class PoisoningSkill : AbstractSlipDamage
    {
        public override SlipDamageCode SlipDamageCode { get; } = SlipDamageCode.Poisoning;
    }
}