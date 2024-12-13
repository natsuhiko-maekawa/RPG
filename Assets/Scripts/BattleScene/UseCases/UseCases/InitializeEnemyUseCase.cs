using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.DataAccesses;
using BattleScene.Domain.Entities;
using BattleScene.Domain.Ids;
using BattleScene.UseCases.IServices;
using Common;
using Utility;
using static BattleScene.Domain.Codes.CharacterTypeCode;

namespace BattleScene.UseCases.UseCases
{
    public class InitializeEnemyUseCase
    {
        private readonly ICharacterCreatorService _characterCreator;
        private readonly IRepository<CharacterEntity, CharacterId> _characterRepository;
        private readonly IEnemySelectorService _enemySelector;
        [ForCache] private readonly List<CharacterEntity> _enemyList = new(Constant.MaxEnemyCount);

        public InitializeEnemyUseCase(
            ICharacterCreatorService characterCreator,
            IRepository<CharacterEntity, CharacterId> characterRepository,
            IEnemySelectorService enemySelector)
        {
            _characterCreator = characterCreator;
            _characterRepository = characterRepository;
            _enemySelector = enemySelector;
        }

        public CharacterEntity[] CreateEnemy()
        {
            // TODO: 仮の値を設定している。
            var enemyTypeCodeArray = new[] { Bee, Dragon, Mantis, Shuten, Slime };
            _enemySelector.SelectEnemy(enemyTypeCodeArray, _enemyList);
            return _enemyList.ToArray();
        }

        public void Initialize()
        {
            _characterRepository.Add(_enemyList);
            _characterCreator.Create(_enemyList.Select(x => x.Id).ToArray());
        }
    }
}