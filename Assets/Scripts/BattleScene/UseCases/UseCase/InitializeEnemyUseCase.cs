using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
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

        public IReadOnlyList<CharacterEntity> Initialize()
        {
            var enemyList = RegisterEnemiesAndGet();
            var enemyIdList = enemyList
                .Select(x => x.Id)
                .ToArray();
            _characterCreator.Create(enemyIdList);
            return enemyList;
        }

        private IReadOnlyList<CharacterEntity> RegisterEnemiesAndGet()
        {
            var enemyTypeCodeList = new[] { Bee, Dragon, Mantis, Shuten, Slime };
            _enemiesRegisterer.Register(enemyTypeCodeList);
            var enemyList = _enemies.Get();
            return enemyList;
        }
    }
}