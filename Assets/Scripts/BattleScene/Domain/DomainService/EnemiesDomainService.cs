using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Aggregate;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;
using Utility;
using Utility.Interface;

namespace BattleScene.Domain.DomainService
{
    public class EnemiesDomainService
    {
        private readonly IPropertyFactory _propertyFactory;
        private readonly IRandomEx _randomEx;
        private readonly IRepository<ActionTimeEntity, CharacterId> _actionTimeRepository;
        private readonly IRepository<CharacterAggregate, CharacterId> _characterRepository;
        private readonly IRepository<EnemyEntity, CharacterId> _enemyRepository;
        private readonly IRepository<HitPointAggregate, CharacterId> _hitPointRepository;

        public EnemiesDomainService(
            IPropertyFactory propertyFactory,
            IRandomEx randomEx, 
            IRepository<ActionTimeEntity, CharacterId> actionTimeRepository,
            IRepository<CharacterAggregate, CharacterId> characterRepository,
            IRepository<EnemyEntity, CharacterId> enemyRepository, 
            IRepository<HitPointAggregate, CharacterId> hitPointRepository)
        {
            _propertyFactory = propertyFactory;
            _randomEx = randomEx;
            _actionTimeRepository = actionTimeRepository;
            _characterRepository = characterRepository;
            _enemyRepository = enemyRepository;
            _hitPointRepository = hitPointRepository;
        }

        public void Add(IList<CharacterTypeId> characterTypeIdList)
        {
            var options = _propertyFactory.Get(characterTypeIdList)
                .Select(x => (Id: x.CharacterTypeId, Parameter: x.SumParameter()))
                .Combination(1, 4)
                .Where(x =>
                {
                    var diff = _propertyFactory.Get(CharacterTypeId.Player).SumParameter() - x.Sum(y => y.Parameter);
                    return diff is >= 0 and <= 5;
                })
                .Select(x => x
                    .Select(y => y.Id)
                    .ToList())
                .ToList();

            var characterList = _randomEx.Choice(options)
                .Select(x =>
                {
                    PropertyValueObject property = _propertyFactory.Get(x);
                    var characterId = new CharacterId();
                    return new CharacterAggregate(characterId, property);
                })
                .ToImmutableList();
            _characterRepository.Update(characterList);

            var hitPointList = characterList
                .Select(x => new HitPointAggregate(x.Id, x.Property.HitPoint))
                .ToImmutableList();
            _hitPointRepository.Update(hitPointList);
            
            var enemyList = characterList
                .OrderBy(x => x.Property.CharacterTypeId)
                .Select((x, i) => new EnemyEntity(x.Id, i))
                .ToImmutableList();
            _enemyRepository.Update(enemyList);
            
            var actionTimeList = characterList
                .Select(x => new ActionTimeEntity(x.Id))
                .ToImmutableList();
            _actionTimeRepository.Update(actionTimeList);
        }
    }
}