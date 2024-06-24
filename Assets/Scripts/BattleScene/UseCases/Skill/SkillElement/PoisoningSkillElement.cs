using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class PoisoningSkillElement : SlipDamageElement
    {
        public override SlipDamageCode GetSlipDamageCode()
        {
            return SlipDamageCode.Poisoning;
        }
    }
}