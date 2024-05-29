using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.UseCase.Skill.AbstractClass
{
    public class ResetSkillElement : ISkillElement
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