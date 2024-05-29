using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.IRepository
{
    public interface IEnemyRepository
    {
        public EnemyEntity Select(CharacterId characterId);
    }
}