using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class BleedingSkillElement : SlipDamageElement
    {
        public override SlipDamageCode GetSlipDamageCode()
        {
            return SlipDamageCode.Bleeding;
        }
    }
}