﻿using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class destroyLeg : AbstractDestroy
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Leg;
        
        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Leg;
        }
    }
}