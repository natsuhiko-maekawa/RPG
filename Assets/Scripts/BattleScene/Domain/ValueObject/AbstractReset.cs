using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.UseCases.Skill.SkillElement.AbstractClass
{
    public class AbstractReset
    {
        public virtual ImmutableList<AilmentCode> GetResetAilment()
        {
            return ImmutableList<AilmentCode>.Empty;
        }

        public virtual ImmutableList<SlipDamageCode> GetResetSlipDamage()
        {
            return ImmutableList<SlipDamageCode>.Empty;
        }

        public virtual ImmutableList<BodyPartCode> GetAidBodyPart()
        {
            return ImmutableList<BodyPartCode>.Empty;
        }
    }
}