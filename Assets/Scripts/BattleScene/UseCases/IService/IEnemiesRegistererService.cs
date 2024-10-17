﻿using System.Collections.Generic;
using BattleScene.Domain.Code;

namespace BattleScene.UseCases.IService
{
    public interface IEnemiesRegistererService
    {
        public void Register(IReadOnlyList<CharacterTypeCode> characterTypeIdList);
    }
}