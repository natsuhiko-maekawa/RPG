using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.InterfaceAdapter.DataAccess.Repository
{
    public class EnemyRepository : IEnemyRepository
    {
        private readonly HashSet<EnemyEntity> _enemySet = new();
        
        public EnemyEntity Select(CharacterId characterId)
        {
            return _enemySet.First(x => Equals(x.CharacterId, characterId));
        }
    }
}