﻿using BattleScene.Domain.Code;
using BattleScene.UseCase.Skill.SkillElement.AbstractClass;

namespace BattleScene.UseCase.Skill.SkillElement
{
    public class ParalysisSkillElement : AilmentSkillElement
    {
        public override AilmentCode GetAilmentCode()
        {
            return AilmentCode.Paralysis;
        }
    }
}