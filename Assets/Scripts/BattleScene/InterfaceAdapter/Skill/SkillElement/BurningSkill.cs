using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class BurningSkill : AbstractSlipDamage
    {
        public override SlipDamageCode GetSlipDamageCode()
        {
            return SlipDamageCode.Burning;
        }
    }
}