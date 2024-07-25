﻿using System.Collections.Immutable;
using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class BurningReset : AbstractReset
    {
        public override ImmutableList<SlipDamageCode> GetResetSlipDamage()
        {
            return ImmutableList.Create(SlipDamageCode.Burning);
        }
    }
}