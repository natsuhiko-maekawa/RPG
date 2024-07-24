using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class Ishinhou : AbstractReset
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