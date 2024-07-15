using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.UseCase.Interface;
using static BattleScene.Domain.Code.CharacterTypeId;

namespace BattleScene.UseCases.UseCase
{
    internal class BattleStart : IUseCase
    {
        private readonly ActionTimeCreatorService _actionTimeCreator;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly CharacterCreatorService _characterCreator;
        private readonly ICharacterRepository _characterRepository;
        private readonly CharactersDomainService _characters;
        private readonly IEnemyRepository _enemyRepository;
        private readonly HitPointCreatorService _hitPointCreator;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;

        public BattleStart(
            ActionTimeCreatorService actionTimeCreator,
            CharacterCreatorService characterCreator,
            CharactersDomainService characters,
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            ICharacterRepository characterRepository,
            IEnemyRepository enemyRepository,
            HitPointCreatorService hitPointCreator,
            IRepository<HitPointAggregate, CharacterId> hitPointRepository)
        {
            _actionTimeCreator = actionTimeCreator;
            _characterCreator = characterCreator;
            _characters = characters;
            _actionTimeRepository = actionTimeRepository;
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
            _hitPointCreator = hitPointCreator;
            _hitPointRepository = hitPointRepository;
        }

        public void Execute()
        {
            var enemyTypeIdList = new List<CharacterTypeId> { Bee, Dragon, Mantis, Shuten, Slime };
            var enemyCharacterList = _characterCreator.CreateEnemyList(enemyTypeIdList);
            _characterRepository.Update(enemyCharacterList);

            var hitPointList = _hitPointCreator.Create(enemyCharacterList);
            _hitPointRepository.Update(hitPointList);
            
            var enemyList = enemyCharacterList
                .OrderBy(x => x.Property.CharacterTypeId)
                .Select((x, i) => new EnemyEntity(x.Id, i))
                .ToImmutableList();
            _enemyRepository.Update(enemyList);

            var actionTimeList = _actionTimeCreator.CreateDefault(_characters.GetIdList());
            _actionTimeRepository.Update(actionTimeList);
        }
    }
}