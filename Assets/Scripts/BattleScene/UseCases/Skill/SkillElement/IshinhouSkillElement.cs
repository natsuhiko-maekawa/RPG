using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCases.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCases.Skill.SkillElement
{
    public class IshinhouSkillElement : ResetSkillElement
    {
        public override ImmutableList<AilmentCode> GetResetAilment()
        {
            return ImmutableList.Create(AilmentCode.Deaf);
        }

        public override ImmutableList<SlipDamageCode> GetResetSlipDamage()
        {
            return ImmutableList.Create(SlipDamageCode.Suffocation, SlipDamageCode.Bleeding);
        }
    }
}