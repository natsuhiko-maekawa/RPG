﻿using BattleScene.Domain.Code;
using BattleScene.InterfaceAdapter.Skill.AbstractClass;

namespace BattleScene.InterfaceAdapter.Skill.SkillElement
{
    public class DestroyArm : AbstractDestroy
    {
        public override BodyPartCode BodyPartCode { get; } = BodyPartCode.Arm;

        public override BodyPartCode GetDestroyPart()
        {
            return BodyPartCode.Arm;
        }
    }
}