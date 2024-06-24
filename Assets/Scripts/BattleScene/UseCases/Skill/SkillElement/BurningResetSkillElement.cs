using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class BurningResetSkillElement : ResetSkillElement
    {
        public override ImmutableList<SlipDamageCode> GetResetSlipDamage()
        {
            return ImmutableList.Create(SlipDamageCode.Burning);
        }
    }
}