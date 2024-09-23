using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.PrimeSkill
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