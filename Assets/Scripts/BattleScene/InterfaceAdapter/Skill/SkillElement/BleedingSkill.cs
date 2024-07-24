using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class BleedingSkill : AbstractSlipDamage
    {
        public override SlipDamageCode GetSlipDamageCode()
        {
            return SlipDamageCode.Bleeding;
        }
    }
}