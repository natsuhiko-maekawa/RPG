using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
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