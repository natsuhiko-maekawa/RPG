using BattleScene.Domain.OldId;

namespace BattleScene.Domain.Entity
{
    public class EnemyEntity : BaseEntity<EnemyEntity, CharacterId>
    {
        public EnemyEntity(
            CharacterId id,
            int enemyNumber)
        {
            Id = id;
            EnemyNumber = enemyNumber;
        }
        
        public override CharacterId Id { get; }
        public int EnemyNumber { get; }
    }
}