using System.Collections.Generic;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface IEnemyRepository
    {
        public EnemyEntity Select(CharacterId characterId);
        public void Update(IList<EnemyEntity> enemyList);
    }
}