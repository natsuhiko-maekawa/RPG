using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class EnemyEntity
    {
        public EnemyEntity(
            CharacterId characterId,
            int enemyNumber)
        {
            CharacterId = characterId;
            EnemyNumber = enemyNumber;
        }
        
        public CharacterId CharacterId { get; }
        public int EnemyNumber { get; }
    }
}