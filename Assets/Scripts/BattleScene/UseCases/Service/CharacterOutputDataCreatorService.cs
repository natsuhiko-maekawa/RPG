using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;

namespace BattleScene.UseCases.Service
{
    public class CharacterOutputDataCreatorService
    {
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;

        public CharacterOutputDataCreatorService(
            IRepository<CharacterEntity, CharacterId> characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public CharacterOutputData Create(CharacterId characterId)
        {
            var character = _characterRepository.Select(characterId);
            return character.IsPlayer
                ? CharacterOutputData.SetPlayer()
                : CharacterOutputData.SetEnemy(_characterRepository.Select(characterId).Position);
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