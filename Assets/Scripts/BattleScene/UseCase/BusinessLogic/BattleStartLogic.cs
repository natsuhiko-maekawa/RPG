using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCase.BusinessLogic.Interface;
using BattleScene.UseCase.Main;
using BattleScene.UseCase.Service;
using static BattleScene.Domain.Code.CharacterTypeId;

namespace BattleScene.UseCase.BusinessLogic
{
    internal class BattleStartLogic : IUseCase
    {
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly CharacterCreatorService _characterCreator;
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly IEnemyRepository _enemyRepository;

        public BattleStartLogic(
            ActionTimeCreatorService actionTimeCreator,
            CharacterCreatorService characterCreator,
            CharactersDomainService characters,
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            ICharacterRepository characterRepository,
            IEnemyRepository enemyRepository)
        {
            _actionTimeCreator = actionTimeCreator;
            _characterCreator = characterCreator;
            _characters = characters;
            _actionTimeRepository = actionTimeRepository;
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
        }

        public void Execute()
        {
            var enemyTypeIdList = new List<CharacterTypeId> { Bee, Dragon, Mantis, Shuten, Slime };
            var enemyCharacterList = _characterCreator.CreateEnemyList(enemyTypeIdList);
            _characterRepository.Update(enemyCharacterList);
            var enemyList = enemyCharacterList
                .OrderBy(x => x.Property.CharacterTypeId)
                .Select((x, i) => new EnemyEntity(x.CharacterId, i))
                .ToImmutableList();
            _enemyRepository.Update(enemyList);

            var actionTimeList = _actionTimeCreator.CreateDefault(_characters.GetIdList());
            _actionTimeRepository.Update(actionTimeList);
        }
    }
}