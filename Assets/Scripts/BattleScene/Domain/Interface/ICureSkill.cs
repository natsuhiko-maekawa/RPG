﻿using BattleScene.Domain.Id;

namespace BattleScene.Domain.Interface
{
    public interface ICureSkill : ISkillElement
    {
        public float GetCureRate();
        public int GetCureAmount(CharacterId targetId);
    }
}