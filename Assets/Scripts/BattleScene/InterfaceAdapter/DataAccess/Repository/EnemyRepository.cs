﻿using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using Utility;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class EnemyRepository : IEnemyRepository
    {
        private readonly HashSet<EnemyEntity> _enemySet = new();
        
        public EnemyEntity Select(CharacterId characterId)
        {
            return _enemySet.First(x => Equals(x.Id, characterId));
        }

        public void Update(IList<EnemyEntity> enemyList)
        {
            foreach (var enemy in enemyList)
                _enemySet.Update(enemy);
        }
    }
}