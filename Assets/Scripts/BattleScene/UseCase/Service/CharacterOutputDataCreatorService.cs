using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCase.Service
{
    public class CharacterOutputDataCreatorService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IEnemyRepository _enemyRepository;

        public CharacterOutputDataCreatorService(
            ICharacterRepository characterRepository,
            IEnemyRepository enemyRepository)
        {
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
        }

        public CharacterOutputData Create(CharacterId characterId)
        {
            var character = _characterRepository.Select(characterId);
            return character.IsPlayer()
                ? CharacterOutputData.SetPlayer()
                : CharacterOutputData.SetEnemy(_enemyRepository.Select(characterId).EnemyNumber);
        }
    }

    public class CharacterOutputData
    {
        private CharacterOutputData()
        {
            IsPlayer = true;
            EnemyNumber = default;
        }

        private CharacterOutputData(int enemyNumber)
        {
            IsPlayer = false;
            EnemyNumber = enemyNumber;
        }

        public bool IsPlayer { get; }
        public int EnemyNumber { get; }

        public static CharacterOutputData SetPlayer()
        {
            return new CharacterOutputData();
        }

        public static CharacterOutputData SetEnemy(int enemyNumber)
        {
            return new CharacterOutputData(enemyNumber);
        }
    }
}