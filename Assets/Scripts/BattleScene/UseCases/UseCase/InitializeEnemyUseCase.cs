using System.Collections.Generic;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Id;
using BattleScene.UseCases.IService;
using static BattleScene.Domain.Code.CharacterTypeCode;

namespace BattleScene.UseCases.UseCase
{
    public class InitializeEnemyUseCase
    {
        private readonly ICharacterCreatorService _characterCreator;
        private readonly EnemiesDomainService _enemies;
        private readonly IEnemiesRegistererService _enemiesRegisterer;
        
        public InitializeEnemyUseCase(
            ICharacterCreatorService characterCreator,
            IEnemiesRegistererService enemiesRegisterer,
            EnemiesDomainService enemies)
        {
            _characterCreator = characterCreator;
            _enemiesRegisterer = enemiesRegisterer;
            _enemies = enemies;
        }

        public void Initialize()
        {
            var enemyIdList = SetEnemies();
            _characterCreator.Create(enemyIdList);
        }

        private IReadOnlyList<CharacterId> SetEnemies()
        {
            var enemyTypeIdList = new[] { Bee, Dragon, Mantis, Shuten, Slime };
            _enemiesRegisterer.Register(enemyTypeIdList);
            var enemyIdList = _enemies.GetId();
            return enemyIdList;
        }
    }
}