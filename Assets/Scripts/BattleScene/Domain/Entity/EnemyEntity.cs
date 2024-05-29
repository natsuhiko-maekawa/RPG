using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class EnemyEntity
    {
        public CharacterId CharacterId { get; }
        public int EnemyNumber { get; }

        public EnemyEntity(
            CharacterId characterId,
            int enemyNumber)
        {
            CharacterId = characterId;
            EnemyNumber = enemyNumber;
        }
    }
}