using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class SuffocationSkillElement : SlipDamageElement
    {
        public override SlipDamageCode GetSlipDamageCode()
        {
            return SlipDamageCode.Suffocation;
        }
    }
}