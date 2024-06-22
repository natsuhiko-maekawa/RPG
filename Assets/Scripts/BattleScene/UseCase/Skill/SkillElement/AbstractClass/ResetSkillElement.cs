﻿using System.Collections.Immutable;
using BattleScene.Domain.Code;

namespace BattleScene.UseCase.Skill.SkillElement.AbstractClass
{
    public class ResetSkillElement : BaseClass.SkillElement
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