﻿using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.UseCase.Skill.Interface
{
    public interface ISlipDamageSkill : ISkillElement
    {
        public SlipDamageCode GetSlipDamageCode();
        public float GetLuckRate();
    }
}