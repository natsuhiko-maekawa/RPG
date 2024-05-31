using BattleScene.Domain.Code;

namespace BattleScene.Domain.ValueObject
{
    public class EnemyViewInfoValueObject
    {
        public EnemyViewInfoValueObject(
            CharacterTypeId characterTypeId,
            string enemyName,
            string enemyImagePath)
        {
            CharacterTypeId = characterTypeId;
            EnemyName = enemyName;
            EnemyImagePath = enemyImagePath;
        }

        public CharacterTypeId CharacterTypeId { get; }
        public string EnemyName { get; }
        public string EnemyImagePath { get; }
    }
}