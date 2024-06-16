using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class HonzougakuSkillElement : ResetSkillElement
    {
        public override ImmutableList<AilmentCode> GetResetAilment()
        {
            return ImmutableList.Create(AilmentCode.Blind, AilmentCode.Paralysis);
        }

        public override ImmutableList<SlipDamageCode> GetResetSlipDamage()
        {
            return ImmutableList.Create(SlipDamageCode.Poisoning);
        }
    }
}