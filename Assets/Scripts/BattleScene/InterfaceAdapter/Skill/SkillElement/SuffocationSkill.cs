using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class SuffocationSkill : AbstractSlipDamage
    {
        public override SlipDamageCode SlipDamageCode { get; } = SlipDamageCode.Suffocation;
    }
}