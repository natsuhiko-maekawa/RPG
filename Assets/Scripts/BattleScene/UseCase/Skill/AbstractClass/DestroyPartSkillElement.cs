﻿using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.UseCase.Skill.Interface;

namespace BattleScene.UseCase.Skill.AbstractClass
{
    public abstract class DestroyPartSkillElement : ISkillElement, ILuckSkillElement
    {
        public float GetLuckRate()
        {
            return 0.5f;
        }

        public abstract BodyPartCode GetDestroyPart();
    }
}