﻿using BattleScene.Domain.Codes;
using BattleScene.InterfaceAdapter.SkillComponents.BaseClass;

namespace BattleScene.InterfaceAdapter.SkillComponents
{
    public class DestroyLeg : BaseDestroy
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Leg;
    }
}