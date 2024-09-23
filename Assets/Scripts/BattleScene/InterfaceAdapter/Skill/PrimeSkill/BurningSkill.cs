using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
{
    public class BurningSkill : AbstractSlipDamage
    {
        public override SlipDamageCode SlipDamageCode { get; } = SlipDamageCode.Burning;
    }
}