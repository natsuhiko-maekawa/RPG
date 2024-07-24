using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class Honzougaku : AbstractReset
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