﻿using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface ISkillRepository
    {
        public SkillEntity Select(CharacterId characterId);
        public void Update(SkillEntity skillEntity);
    }
}